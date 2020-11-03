using stack.Helper;
using stack.Models.Options;

namespace stack
{
    public static class StackEnvironments
    {
        public static class Keys
        {
            public const string ConnectionString = "STACK_CONNECTION_STRING";

            public const string JwtAuthOptions = "STACK_JWT_AUTH_OPTIONS";

            public const string CookieAuthOptions = "STACK_COOKIE_AUTH_OPTIONS";
        }

        // Variables
        private static string _connectionString = "";
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = EnvironmentHelper.GetValueAsString(Keys.ConnectionString);

                return _connectionString;
            }
        }

        private static JwtAuthOptions _jwtAuthOptions = null;
        public static JwtAuthOptions JwtAuthOptions
        {
            get
            {
                if (_jwtAuthOptions == null)
                    _jwtAuthOptions = EnvironmentHelper.GetValueAsObject<JwtAuthOptions>(Keys.JwtAuthOptions);

                return _jwtAuthOptions;
            }
        }

        private static CookieAuthOptions _cookieAuthOptions = null;
        public static CookieAuthOptions CookieAuthOptions
        {
            get
            {
                if (_cookieAuthOptions == null)
                    _cookieAuthOptions = EnvironmentHelper.GetValueAsObject<CookieAuthOptions>(Keys.CookieAuthOptions);

                return _cookieAuthOptions;
            }
        }
    }
}
