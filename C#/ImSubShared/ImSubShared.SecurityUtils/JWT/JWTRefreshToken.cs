using System;

namespace ImSubShared.SecurityUtils.JWT
{
    public class JWTRefreshToken
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ExpDateTime { get; set; }
        public bool IsValid => RevokedAt == null && ExpDateTime >= DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }
        public string Token { get; set; }

    }
}
