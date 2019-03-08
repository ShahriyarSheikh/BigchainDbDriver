using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.DataEncoders;
using Chaos;
using Nacl;
using Chaos.NaCl;
using NBitcoin;
using System.Linq;

namespace BigchainDbDriver.KeyPair
{
    public class Ed25519Keypair
    {
        private readonly string publicKey = "46HZkdTMr4MAwgYbXaqBaztPvzvutyEVBp91EuPPonHU";
        public Ed25519Keypair()
        {

        }
        public dynamic GenerateKeyPair(dynamic seed) {
            DataEncoder encoder = Encoders.Base58;
            Mnemonic mn = new Mnemonic(Wordlist.English,WordCount.Twelve);
            var byt = mn.DeriveSeed();

            var a = Guid.NewGuid() ;
            //var result  = a.ToByteArray().Concat(new byte[16]);
            

            var pk = Ed25519.PublicKeyFromSeed(byt);
            var sk = Ed25519.ExpandedPrivateKeyFromSeed(byt);

            var _sk = SliceMe(sk,32);
            return new
            {
                privateKey = encoder.EncodeData(_sk),
                publicKey = encoder.EncodeData(pk)
            };

            //return encoder.EncodeData(Convert.FromBase64String(publicKey));
            
        }

        private byte[] SliceMe(byte[] source, int length)
        {
            byte[] destfoo = new byte[length];
            Array.Copy(source, 0, destfoo, 0, length);
            return destfoo;
        }
    }
}
