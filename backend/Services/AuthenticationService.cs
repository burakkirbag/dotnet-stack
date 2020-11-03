using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using stack.Data;
using stack.Models;
using stack.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace stack.Services
{
    public interface IAuthenticationService
    {
        Task<ServiceReturn<AccessTokenDto>> Login(string email, string password, string role);
        Task<ServiceReturn<AccessTokenDto>> CreateToken(User user);
    }

    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly UserManager<User> _userManager;

        public AuthenticationService(StackDbContext db, UserManager<User> userManager) : base(db)
        {
            _userManager = userManager;
        }

        public async Task<ServiceReturn<AccessTokenDto>> Login(string email, string password, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Failed<AccessTokenDto>("Girmiş olduğunuz e-posta adresi yanlıştır.");

            var checkPassword =
               _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (checkPassword == PasswordVerificationResult.Failed)
                return Failed<AccessTokenDto>("Girmiş olduğunuz şifre yanlıştır.");

            var checkRole = await _userManager.IsInRoleAsync(user, role);
            if (!checkRole)
                return Failed<AccessTokenDto>("Bu işlemi gerçekleştirmek için yetkiniz bulunmuyor.");

            var token = await GenerateToken(user);

            return Success<AccessTokenDto>(token);
        }

        public async Task<ServiceReturn<AccessTokenDto>> CreateToken(User user)
        {
            var token = await GenerateToken(user);

            return Success<AccessTokenDto>(token);
        }

        private async Task<AccessTokenDto> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var expireTime = DateTime.UtcNow.AddDays(StackEnvironments.JwtAuthOptions.TokenValidityDay);

            var claims = new List<Claim>() {
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim (JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StackEnvironments.JwtAuthOptions.SigningKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: StackEnvironments.JwtAuthOptions.Issuer,
                audience: StackEnvironments.JwtAuthOptions.Audience,
                claims: claims,
                expires: expireTime,
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.WriteToken(jwtSecurityToken);

            return new AccessTokenDto { AccessToken = token, ExpireTime = expireTime };
        }
    }
}
