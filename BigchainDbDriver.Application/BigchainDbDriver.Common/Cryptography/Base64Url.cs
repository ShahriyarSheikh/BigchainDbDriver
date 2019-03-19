using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Common.Cryptography
{
    public static class Base64Url
    {
        public static byte[] Decode(string base64UrlString) {

            var stringToDecode = base64UrlString.Replace("-", "+").Replace("_", "/");
            return Base64UrlCore.Base64Url.Decode(stringToDecode).ToByteArray();
        }

        public static string Encode(byte[] buffer) {
            return Base64UrlCore.Base64Url.Encode(buffer).Replace("=", "").Replace("+", "-").Replace("/","_");
        }
    }
}
