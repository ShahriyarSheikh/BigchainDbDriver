using SHA3.Net;
using System;
using System.Collections.Generic;
using System.Text;
using SHA3.Net;

namespace BigchainDbDriver.Common.Cryptography
{
    public class Sha3_256
    {
        public string ComputeHash(byte[] buffer) {
            var sha = Sha3.Sha3256();
            var result = sha.ComputeHash(buffer);
            return result.ToHex();
        }

        public string ComputeHash(string str) {
            var buffer = Encoding.ASCII.GetBytes(str);
            var sha = Sha3.Sha3256();
            var result = sha.ComputeHash(buffer);
            return result.ToHex();
        }
    }
}
