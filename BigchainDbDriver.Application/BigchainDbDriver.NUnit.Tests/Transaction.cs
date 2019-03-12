using BigchainDbDriver.Assets.Models;
using BigchainDbDriver.KeyPair;
using BigchainDbDriver.Transactions;
using NUnit.Framework;
using System.Collections.Generic;

namespace BigchainDbDriver.NUnit.Tests
{
    [TestFixture]
    class Transaction
    {
        private readonly GeneratedKeyPair generatedKeyPair;
        private dynamic _kyc = new
        {
                    pob = "",
                    dob = "",
                    nab = "",
                    user_hash = ""
        };

        private dynamic metadata = new
        {
            error = string.Empty,
            status="A",
            transaction = string.Empty
        };

        public Transaction()
        {
            var keypair = new Ed25519Keypair();
            generatedKeyPair = keypair.GenerateKeyPair();
        }


        [Test]
        public void Provided_Input_Payload_Metadata_Keys_MakeCreateTransction() {


            Bigchain_MakeCreateTransaction transaction = new Bigchain_MakeCreateTransaction();

            TxTemplate txTemplate = transaction.MakeCreateTransaction(_kyc,
                metadata,
                transaction.MakeOutput(transaction.MakeEd25519Condition(generatedKeyPair.PublicKey)),
                new List<string> { generatedKeyPair.PublicKey }
                );

            Assert.AreEqual(generatedKeyPair.PublicKey,txTemplate.Outputs[0].PublicKeys[0]);
            Assert.AreEqual(generatedKeyPair.PublicKey, txTemplate.Inputs[0].Owners_before[0]);
        }

    }
}
