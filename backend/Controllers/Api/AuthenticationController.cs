using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stack.Data;
using stack.Models;
using stack.Models.DTOs;
using stack.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stack.Controllers.Api
{
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(ApiReturn<RegisterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiReturn<List<string>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiReturn<Exception>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterInput input)
        {
            var user = new User
            {
                Firstname = input.Firstname,
                Lastname = input.Lastname,
                Email = input.Email,
                UserName = input.Email
            };

            var createdUser = await _userService.Create(user, input.Password);
            if (!createdUser.Success)
                return BadRequest<List<string>>(createdUser.Errors, "Kayıt işleminiz tamamlanamadı. Lütfen girmiş olduğunuz bilgileri kontrol edin.");

            var addedRole = await _userService.AddToRole(user, "User");
            if (!addedRole.Success)
                return BadRequest<List<string>>(addedRole.Errors, "Kayıt işleminiz tamamlanamadı. Lütfen girmiş olduğunuz bilgileri kontrol edin.");

            var createdToken = await _authenticationService.CreateToken(user);
            if (!createdToken.Success)
                return BadRequest<List<string>>(createdToken.Errors, "Kayıt işleminiz başarıyla tamamlandı, ancak token oluşturulma sırasında bir hata oluştu.");

            var data = new RegisterDto
            {
                UserId = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                AccessToken = createdToken.Data
            };

            return Success<RegisterDto>(data, "Kayıt işleminiz başarıyla tamamlandı.");
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ApiReturn<AccessTokenDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiReturn<List<string>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiReturn<Exception>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginInput input)
        {
            var login = await _authenticationService.Login(input.Email, input.Password, "User");
            if (!login.Success)
                return BadRequest<AccessTokenDto>(null, login.Message);

            return Success(login.Data, "Başarıyla giriş yaptınız.");
        }
    }
}
