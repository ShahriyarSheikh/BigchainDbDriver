using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using System;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;

namespace BigchainDbDriver.Common.Cryptography
{
    public class Asn1lib 
    {
        private readonly byte[] _publicKey;
        private readonly byte[] _signature;
        private const string TYPE_ASN1_FULFILLMENT = "ed25519Sha256Fulfillment";
        private readonly byte[] checking = new byte[] {
            164,100,128,32,188,164,175,37,42,70,241,134,75,6,132,76,251,211,122,145,56,150,41,41,129,185,89,
            201,220,175,16,41,102,17,235,154,129,64,65,103,211,74,215,107,15,14,156,67,45,241,223,243,6,62,204,168,198,37,
            78,11,161,180,12,23,240,147,233,104,148,0,135,64,43,61,152,30,10,238,67,221,65,22,36,96,136,1,235,165,198,112,86,
            224,14,65,28,157,248,221,3,84,80,7
        };

        public Asn1lib(byte[] publicKey, byte[] signature)
        {
            _publicKey = publicKey;
            _signature = signature;
        }
        //http://www.bouncycastle.org/csharp/index.html
        //TODO: Figure out asn1 der encoding and return it from this function
        public byte[] SerializeBinary() {
            var res = GetAsn1Json();

            //var v = new Asn1EncodableVector();
            //v.Add(new DerOctetString(res.Value.PublicKey));
            //v.Add(new DerOctetString(res.Value.Signature));

            //TODO: Figure out why sequence is not generating proper fulfillmentUri
            MemoryStream bOut = new MemoryStream();
            DerSequenceGenerator seqGen1 = new DerSequenceGenerator(bOut);
            
            seqGen1.AddObject(new DerOctetString(res.Value.PublicKey));
            //seqGen1.AddObject(v[0]);

            //DerSequenceGenerator seqGen2 = new DerSequenceGenerator(seqGen1.GetRawOutputStream(), 4, false);

            seqGen1.AddObject(new DerOctetString(res.Value.Signature));
            //seqGen2.AddObject(v[1]);

            //seqGen2.Close();

            seqGen1.Close();


            var chec1 = bOut.ToArray();


            var obj = Asn1Object.FromByteArray(chec1);
            var asn1encoder =  obj.GetDerEncoded();
            return asn1encoder;
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
