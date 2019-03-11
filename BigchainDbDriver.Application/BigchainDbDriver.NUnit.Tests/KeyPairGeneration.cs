using BigchainDbDriver.KeyPair;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.NUnit.Tests
{
    [TestFixture]
    class KeyPairGeneration
    {
        [Test]
        public void OnSeedInput_GivesKeyPair() {

            var expectedPrivKey = "11111111111111111111111111111111";
            var expectedPubKey = "4zvwRjXUKGfvwnParsHAS3HuSVzV5cA4McphgmoCtajS";
            var seed = new byte[32];

            var ed25519Keypair = new Ed25519Keypair();

            var key = ed25519Keypair.GenerateKeyPair(seed);

            Assert.AreEqual(expectedPrivKey, key.PrivateKey,"Private key must be same");

            Assert.AreEqual(expectedPubKey, key.PublicKey,"Public key must be same");

            key = ed25519Keypair.GenerateKeyPair();

            Assert.AreNotEqual(expectedPrivKey, key.PrivateKey, "Private key must not be same");

            Assert.AreNotEqual(expectedPubKey, key.PublicKey, "Public Key must not be same");

        }
    }
}
