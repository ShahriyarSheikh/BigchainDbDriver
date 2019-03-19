using Base64UrlCore;
using Chaos.NaCl;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Common.Cryptography
{
    public static class CryptographyUtility
    {
        public static string EncodeToBase64Url(this string str) {
            return Base64Url.Encode(str);
        }

        public static byte[] Ed25519Sign(byte[] pubKey, byte[] expandedPrivKey) {

            return Ed25519.Sign(pubKey, expandedPrivKey);
        }

        public static string SerializeUri(byte[] serializedJson) {
            return EncodeToBase64Url(Convert.ToBase64String(serializedJson));
        }

        public static bool VerifySignature(this byte[] signature, byte[] message, byte[] publicKey) {
            return Ed25519.Verify(signature, message, publicKey);
        }
    }
}
