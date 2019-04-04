using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BigchainDbDriver.Common.Cryptography
{
    public static class HashingUtils
    {
        public static string GenerateSha256Hash<T>(T obj) {

            var serializedMessage = JsonConvert.SerializeObject(obj);
            var bytesToSign = Encoding.UTF8.GetBytes(serializedMessage);

            byte[] hash;

            using (var sha = new SHA256Managed()) {
                hash = sha.ComputeHash(bytesToSign, 0, bytesToSign.Length);
            }

            return Convert.ToBase64String(hash);
        }

        public static string GenerateSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string SHA256HexHashString(string rawData)
        {
            string hashString;
            using (var sha256 = SHA256Managed.Create())
            {
                var hash = sha256.ComputeHash(Encoding.Default.GetBytes(rawData));
                hashString = hash.ToHex(false);
            }

            return hashString;
        }

        public static byte[] ComputeSha256Hash(byte[] data)
        {
            var shaHash = new Sha256Digest();
            shaHash.BlockUpdate(data, 0, data.Length);
            byte[] hashedValue = new byte[shaHash.GetDigestSize()];
            shaHash.DoFinal(hashedValue, 0);
            return hashedValue;
        }

        public static byte[] ComputeSha3Hash(byte[] data)
        {
            var shaHash = new Sha3Digest();
            shaHash.BlockUpdate(data, 0, data.Length);
            byte[] hashedValue = new byte[shaHash.GetDigestSize()];
            shaHash.DoFinal(hashedValue, 0);
            return hashedValue;
        }
    }
}
