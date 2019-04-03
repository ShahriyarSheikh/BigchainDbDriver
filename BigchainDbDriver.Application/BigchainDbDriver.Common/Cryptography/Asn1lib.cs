using Chaos.NaCl;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace BigchainDbDriver.Common.Cryptography
{
    public class Asn1lib
    {
        private readonly byte[] _publicKey;
        private readonly byte[] _signature;
        private const string TYPE_ASN1_FULFILLMENT = "ed25519Sha256Fulfillment";

        public Asn1lib(byte[] publicKey)
        {
            _publicKey = publicKey;
        }

        //http://www.bouncycastle.org/csharp/index.html
        public byte[] SerializeBinary(byte[] signature)
        {
            using (var bOut = new MemoryStream())
            {
                DerSequenceGenerator seqGen1 = new DerSequenceGenerator(bOut, 4, false);

                seqGen1.AddObject(new DerTaggedObject(false, 0, new DerOctetString(_publicKey)));

                seqGen1.AddObject(new DerTaggedObject(false, 1, new DerOctetString(signature)));

                seqGen1.Close();

                var encoded = bOut.ToArray();

                return encoded;
            }
        }

        public byte[] GetFingerprint()
        {
            var fingerPrintBytes = GetFingerprintBytes();

            var b64 = string.Join(',', fingerPrintBytes);

            return ComputeSHA256Hash(fingerPrintBytes);
        }

        private byte[] GetFingerprintBytes()
        {
            using (var bOut = new MemoryStream())
            {
                DerSequenceGenerator seqGen1 = new DerSequenceGenerator(bOut);
                
                seqGen1.AddObject(new DerTaggedObject(false, 0, new DerOctetString(_publicKey)));

                seqGen1.Close();                

                var encoded = bOut.ToArray();

                return encoded;
            }
        }

        private static byte[] ComputeSHA256Hash(byte[] data)
        {
            var shaHash = new Sha256Digest();
            shaHash.BlockUpdate(data, 0, data.Length);
            byte[] hashedValue = new byte[shaHash.GetDigestSize()];
            shaHash.DoFinal(hashedValue, 0);
            return hashedValue;
        }

        private dynamic GetEncoder(string encoderType = null)
        {
            var enc = encoderType ?? "der";
            return enc;
        }

    }

    [Serializable]
    public class Asn1Json
    {

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public Asn1JsonPayload Value { get; set; }

    }

    [Serializable]
    public class Asn1JsonPayload
    {
        [JsonProperty("publicKey")]
        public byte[] PublicKey { get; set; }
        [JsonProperty("signature")]
        public byte[] Signature { get; set; }
    }
}
