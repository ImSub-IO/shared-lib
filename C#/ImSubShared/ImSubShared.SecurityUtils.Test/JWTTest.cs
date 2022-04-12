using ImSubShared.SecurityUtils.JWT;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.SecurityUtils.Test
{
    [TestClass]
    public class JWTTest
    {
        private static JWTTokenInfo tokenInfo = new JWTTokenInfo
        {
            Issuer = "test_issuer",
            Audience = "test_audience",
            ValidMinutes = 10,
            Key = "TESTKEY_hdhsjfh4u537486rgsfsd74573ysjfklsdjf487543skjfl"
        };

        [TestMethod]
        public void ValidateJWTNoExp_ValidToken()
        {
            var tokenHandler = new JWTTokenHandler();
            var token = tokenHandler.GenerateJWTToken(tokenInfo, null);
            var result = tokenHandler.ValidateJWTNoExp(token, tokenInfo);
            Assert.AreEqual(true, result.valid);
            Assert.IsNull(result.exception);
        }

        [TestMethod]
        public void ValidateJWTNoExp_ValidExpiredToken()
        {
            var tokenHandler = new JWTTokenHandler();
            tokenInfo.ValidMinutes = 0;
            var token = tokenHandler.GenerateJWTToken(tokenInfo, null);
            var result = tokenHandler.ValidateJWTNoExp(token, tokenInfo);
            tokenInfo.ValidMinutes = 10;
            Assert.AreEqual(true, result.valid);
            Assert.IsNull(result.exception);
        }

        [TestMethod]
        public void ValidateJWTNoExp_InvalidSign()
        {
            var tokenHandler = new JWTTokenHandler();
            var token = tokenHandler.GenerateJWTToken(tokenInfo, null);
            token = token.Remove(token.Length - 1) + "H";
            var result = tokenHandler.ValidateJWTNoExp(token, tokenInfo);
            Assert.AreEqual(false, result.valid);
            Assert.IsNotNull(result.exception);
            Assert.AreEqual(typeof(SecurityTokenSignatureKeyNotFoundException).Name, result.exception.GetType().Name);
        }

        [TestMethod]
        public void ValidateJWTNoExp_InvalidIssuer()
        {
            var tokenHandler = new JWTTokenHandler();
            var token = tokenHandler.GenerateJWTToken(tokenInfo, null);
            tokenInfo.Issuer = "Pippo";
            var result = tokenHandler.ValidateJWTNoExp(token, tokenInfo);
            tokenInfo.Issuer = "test_audience";
            Assert.AreEqual(false, result.valid);
            Assert.IsNotNull(result.exception);
            Assert.AreEqual(typeof(SecurityTokenInvalidIssuerException).Name, result.exception.GetType().Name);
        }

    }
}
