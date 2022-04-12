using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ImSubShared.SecurityUtils.JWT
{
    public class JWTTokenHandler
    {
        /// <summary>
        /// Create a new <code>JWTRefreshToken</code>
        /// </summary>
        /// <param name="expDays">Days from expiration</param>
        /// <returns>A new <code>JWTRefreshToken</code></returns>
        public JWTRefreshToken GenerateRefreshToken(int expDays)
        {
            var randomBytes = new byte[32];
            using (var rndNumberGenerator = RandomNumberGenerator.Create())
            {
                rndNumberGenerator.GetBytes(randomBytes);
                return new JWTRefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpDateTime = DateTime.UtcNow.AddDays(expDays),
                    CreatedAt = DateTime.UtcNow
                };
            }
        }

        /// <summary>
        /// Create a new <code>JwtSecurityToken</code>
        /// </summary>
        /// <param name="tokenInfo"></param>
        /// <param name="key"></param>
        /// <returns>A the new token</returns>
        public string GenerateJWTToken(JWTTokenInfo tokenInfo, Claim[] claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenInfo.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenInfo.Issuer,
                audience: tokenInfo.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tokenInfo.ValidMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// Used to partially validate a JWT token. It doesn't validate the expiration
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenInfo"></param>
        /// <returns>(true, null) if valid, (false, <see cref="Exception"/>) otherwise</returns>
        public (bool valid, Exception exception)  ValidateJWTNoExp(string token, JWTTokenInfo tokenInfo)
        {
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = tokenInfo.Audience,
                    ValidIssuer = tokenInfo.Issuer,
                    ValidateLifetime = false,
                    ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenInfo.Key))
                }, out SecurityToken securityToken);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }
    }
}
