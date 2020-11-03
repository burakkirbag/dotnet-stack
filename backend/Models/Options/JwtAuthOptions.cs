namespace stack.Models.Options
{
    public class JwtAuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenValidityDay { get; set; }
        public string SigningKey { get; set; }
        public string RoleClaimType { get; set; }
    }
}
