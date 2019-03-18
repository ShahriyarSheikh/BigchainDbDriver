using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BigchainDbDriver.Common.Cryptography
{
    public class Sha256
    {
        public Sha256()
        {

        }

        public string GenerateSha256Hash<T>(T obj) {

            var serializedMessage = JsonConvert.SerializeObject(obj);
            var bytesToSign = Encoding.UTF8.GetBytes(serializedMessage);

            byte[] hash;

            using (var sha = new SHA256Managed()) {
                hash = sha.ComputeHash(bytesToSign, 0, bytesToSign.Length);
            }

            return Convert.ToBase64String(hash);
        }

        public string GenerateSha256Hash(string rawData) {
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

        private string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }

        public string SHA256HexHashString(string rawData)
        {
            string hashString;
            using (var sha256 = SHA256Managed.Create())
            {
                var hash = sha256.ComputeHash(Encoding.Default.GetBytes(rawData));
                hashString = ToHex(hash, false);
            }

            return hashString;
        }
    }
}
