using System;

namespace stack.Models.DTOs
{
    public class RegisterDto
    {
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public AccessTokenDto AccessToken { get; set; }
    }
}
