using System;
using NBitcoin.DataEncoders;
using Chaos.NaCl;
using NBitcoin;
using BigchainDbDriver.Assets.Models;

namespace BigchainDbDriver.KeyPair
{
    public class Ed25519Keypair : IEd25519Keypair
    {
        private readonly int bytesSupportedbyEd25519 = 32;
        public Ed25519Keypair()
        {

        }
        public GeneratedKeyPair GenerateKeyPair(byte[] seed = null)
        {
            DataEncoder encoder = Encoders.Base58;

            var byt = seed == null ? RandomUtils.GetBytes(bytesSupportedbyEd25519) : seed;

            var pk = Ed25519.PublicKeyFromSeed(byt);

            var sk = Ed25519.ExpandedPrivateKeyFromSeed(byt);

            // tweetnacl's generated secret key is the secret key + public key (resulting in a 64-byte buffer)
            // therefore require slicing
            var _sk = Slice(sk, bytesSupportedbyEd25519);

            return new GeneratedKeyPair
            {
                PrivateKey = encoder.EncodeData(_sk),
                PublicKey = encoder.EncodeData(pk),
                ExpandedPrivateKey = encoder.EncodeData(sk)
            };
        }

        private byte[] Slice(byte[] source, int length)
        {
            byte[] destfoo = new byte[length];
            Array.Copy(source, 0, destfoo, 0, length);
            return destfoo;
        }
    }
}
