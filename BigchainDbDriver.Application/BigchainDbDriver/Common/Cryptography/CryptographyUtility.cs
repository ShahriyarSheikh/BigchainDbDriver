using Chaos.NaCl;

namespace BigchainDbDriver.Common.Cryptography
{
    public static class CryptographyUtility
    {

        public static byte[] Ed25519Sign(byte[] pubKey, byte[] expandedPrivKey) {

            return Ed25519.Sign(pubKey, expandedPrivKey);
        }

        public static string SerializeUri(byte[] serializedJson) {
            return Base64Url.Encode(serializedJson);
        }

        public static bool VerifySignature(this byte[] signature, byte[] message, byte[] publicKey) {
            return Ed25519.Verify(signature, message, publicKey);
        }
    }
}
