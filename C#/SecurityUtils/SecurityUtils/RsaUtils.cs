using System;
using System.Security.Cryptography;
using System.Text;

namespace SecurityUtils
{
    public class RsaUtils
    {
        private string _modulus;
        private string _exponent;
        public RsaUtils(string modulus, string exponent)
        {
            _modulus = modulus;
            _exponent = exponent;
        }

        /// <summary>
        /// Verify RS256 signature
        /// </summary>
        /// <param name="stringSigned"></param>
        /// <param name="sign"></param>
        /// <returns><code>true</code> if the sign is valid, <code>true</code> otherwise</returns>
        public bool VerifyRS256Signature(string stringSigned, string sign)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(
              new RSAParameters()
              {
                  Modulus = FromBase64Url(_modulus),
                  Exponent = FromBase64Url(_exponent)
              });

            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(stringSigned));

                RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");

                return rsaDeformatter.VerifySignature(hash, FromBase64Url(sign));
            }
        }

        public static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }
    }
}
