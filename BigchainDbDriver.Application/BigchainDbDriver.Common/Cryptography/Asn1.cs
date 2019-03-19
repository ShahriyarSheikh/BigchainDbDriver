using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BigchainDbDriver.Common.Cryptography
{
    public class Asn1 
    {
        private readonly byte[] _publicKey;
        private readonly byte[] _signature;
        private const string TYPE_ASN1_FULFILLMENT = "ed25519Sha256Fulfillment";

        public Asn1(byte[] publicKey, byte[] signature)
        {
            _publicKey = publicKey;
            _signature = signature;
        }

        //TODO: Figure out asn1 der encoding and return it from this function
        public dynamic SerializeBinary() {

            return GetAsn1Json();
        }


        private byte[] ObjectToByteArray(object obj) {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }


        private dynamic GetEncoder(string encoderType = null) {
            var enc = encoderType ?? "der";
            return enc;
        }

        private Asn1Json GetAsn1Json() {
            return new Asn1Json {
                Type = TYPE_ASN1_FULFILLMENT,
                Value = new Asn1JsonPayload {
                    PublicKey = this._publicKey,
                    Signature = _signature
                }
            };
        }


    }

    [Serializable]
    public class Asn1Json {

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public Asn1JsonPayload Value { get; set; }

    }

    [Serializable]
    public class Asn1JsonPayload {
        [JsonProperty("publicKey")]
        public byte[] PublicKey { get; set; }
        [JsonProperty("signature")]
        public byte[] Signature { get; set; }
    }
}
