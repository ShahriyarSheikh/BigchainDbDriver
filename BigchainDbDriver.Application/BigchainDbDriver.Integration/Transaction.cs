using BigchainDbDriver.Assets.Models.TransactionModels;
using BigchainDbDriver.Common;
using BigchainDbDriver.General;
using BigchainDbDriver.Transactions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BigchainDbDriver.Integration
{
    class Transaction
    {
        private readonly string bigchainhost = "http://192.168.100.10:9984/api/v1/";

        [Ignore("Integration only")]
        [Test]
        public async Task ProvidedSignedTx_ShouldPostCommitTransaction()
        {
            var signedTx = GetMockResponseSignedTx();

            var connection = new BigchainConnection(bigchainhost);
            var (response, status) = await connection.PostTransactionCommit(signedTx);

            Assert.AreNotEqual(status, HttpStatusCode.BadRequest);
            Assert.That(status == HttpStatusCode.Accepted || status == HttpStatusCode.Created || status == HttpStatusCode.NoContent);
        }

        [Ignore("Integration Only")]
        [Test]
        public void Provided_Input_Should_Create_And_Make_Transfer_Transaction() {

            Bigchain_Transaction transaction = new Bigchain_Transaction();
            var metadata = new Metadata
            {
                Error = "",
                Status = "",
                Transaction = ""
            };

            var block = GetMockResponseTx("45qKQwNdc7qGfkLaKyxK3PwKDTv9S1PdmFLSLX5rfd6k"); //Should come from getLatestBlock(id)

            var unspentOutput = new UnspentOutput {
                Tx = block,
                OutputIndex = 0
            };

            //MakeOutput(makeEd25519condition(pubkey))
            var Outputs = new List<Output>() {
                    new Output{
                        Amount = "1",
                        Condition = Asn1ConditionsHelper.MakeEd25519Condition(block.Outputs[0].PublicKeys[0]),
                        PublicKeys = new List<string>(){
                            block.Outputs[0].PublicKeys[0]
                        }
                    }
                };


            var tx = transaction.MakeTransferTransaction(new List<UnspentOutput> { unspentOutput },
                Outputs,
                metadata
                );

            Assert.Pass();
        }


        private SignedTxResponse GetMockResponseSignedTx()
        {
            const string pubKey = "EN6jFN4LAaBnzkZQekdzYU5XUTyKKX5EiUUBnFgfkozQ";

            return new SignedTxResponse
            {
                id = "d48b333ea27d60dae01546a3a184d532e7fad7c7545335ac7d0a32b0fe517a71",
                asset = new AssetDefinition
                {
                    Data = new DataDefinition
                    {
                        Kyc = new KycDefinition
                        {
                            Dob = "11/23/1995 12:00:00 AM +00:00",
                            Nab = "JohnDoe2",
                            Pob = "PK",
                            UserHash = "5c86551688dbd41fdc9ed303"
                        }
                    }

                },
                inputs = new List<InputTemplate>() {
                   new InputTemplate{
                       Fulfills = null,
                       Fulfillment = "pGSAIMaPmqqCAswrUdxfzjgqRQGaIQN8M3yBO2LJoSlZRQXxgUBUm9G4vE7Xy-b4YbHyYAYQOSUJBi5ejXRExz9rflb4LVx6wYgrewwR89TeLC-HeuxbjuckZj7-z37NDPXaw8EB",
                       Owners_before = new List<string>(){ "EN6jFN4LAaBnzkZQekdzYU5XUTyKKX5EiUUBnFgfkozQ" }
                   }
                },
                metadata = new Metadata
                {
                    Error = null,
                    Status = "A",
                    Transaction = null
                },
                operation = "CREATE",
                version = "2.0",
                outputs = new List<Output>() {
                    new Output{
                        Amount = "1",
                        Condition = Asn1ConditionsHelper.MakeEd25519Condition(pubKey),
                        PublicKeys = new List<string>(){
                            pubKey
                        }
                    }
                }
            };
        }

        private TxTemplate GetMockResponseTx(string pubKey)
        {
            return new TxTemplate
            {
                Id = "282137f67ce65e34a9eb13145606d7bf87bdaf9174111d6053002bfc543575c8",
                Asset = new AssetDefinition
                {
                    Data = new DataDefinition
                    {
                        Kyc = new KycDefinition
                        {
                            Dob = "7/19/1988 12:00:00 AM +05:00",
                            Nab = "Hang MioLoi",
                            Pob = "CN",
                            UserHash = "5c9b0ddd16f0d6471c661c0e"
                        }
                    }

                },
                Inputs = new List<InputTemplate>() {
                    new InputTemplate {
                        Fulfills = null,
                        Fulfillment = null,
                        Owners_before = new List<string>() { pubKey }
                    }
                },
                Metadata = new Metadata
                {
                    Error = null,
                    Status = "A",
                    Transaction = null
                },
                Operation = "CREATE",
                Version = "2.0",
                Outputs = new List<Output>() {
                    new Output{
                        Amount = "1",
                        Condition = Asn1ConditionsHelper.MakeEd25519Condition(pubKey),
                        PublicKeys = new List<string>(){
                            pubKey
                        }
                    }
                }
            };
        }

    }
}
