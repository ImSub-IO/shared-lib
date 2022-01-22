using System.Security.Cryptography;

namespace SecurityUtils
{
    public class HashUtils
    {
        /// <summary>
        /// Compute the SHA256 hash value
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ComputeSHA256(byte[] data)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                return hasher.ComputeHash(data);
            }
        }

        /// <summary>
        /// Compute the HMACSHA256 hash value
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] ComputeHMACSHA256(byte[] data, byte[] key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(data);
            }
        }
    }
}
