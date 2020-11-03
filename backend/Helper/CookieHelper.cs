using Microsoft.AspNetCore.Http;
using System;

namespace stack.Helper
{
    public static class CookieHelper
    {
        public static void Set(this HttpContext context, string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddDays(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMinutes(30);

            context.Response.Cookies.Append(key, value, option);
        }

        public static void Remove(this HttpContext context, string key)
        {
            context.Response.Cookies.Delete(key);
        }
    }
}
