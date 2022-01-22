using System.Security.Claims;

namespace SecurityUtils.JWT
{
    public class JWTTokenInfo
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
        public int ValidMinutes { get; set; }

    }
}
