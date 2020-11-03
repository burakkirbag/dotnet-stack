using System;

namespace stack.Models.DTOs
{
    public class AccessTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
