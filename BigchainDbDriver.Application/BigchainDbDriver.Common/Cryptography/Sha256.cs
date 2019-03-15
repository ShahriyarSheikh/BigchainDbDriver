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

        public string GenerateSha256Hash(string serializedMessage) {
            var bytesToSign = Encoding.UTF8.GetBytes(serializedMessage);

            byte[] hash;

            using (var sha = new SHA256Managed())
            {
                hash = sha.ComputeHash(bytesToSign, 0, bytesToSign.Length);
            }

            return Convert.ToBase64String(hash);
        }
    }
}
