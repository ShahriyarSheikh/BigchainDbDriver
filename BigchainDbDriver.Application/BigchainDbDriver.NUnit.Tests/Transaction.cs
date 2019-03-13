﻿using BigchainDbDriver.Assets.Models;
using BigchainDbDriver.Assets.Models.TransactionModels;
using BigchainDbDriver.General;
using BigchainDbDriver.KeyPair;
using BigchainDbDriver.Transactions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BigchainDbDriver.NUnit.Tests
{
    [TestFixture]
    class Transaction
    {
        private readonly GeneratedKeyPair generatedKeyPair;
        private readonly string bigchainhost = "http://192.168.100.10:9984/api/v1/";

        public Transaction()
        {
            var keypair = new Ed25519Keypair();
            generatedKeyPair = keypair.GenerateKeyPair();
        }


        [Test, Order(1)]
        public void Provided_Input_Payload_Metadata_Keys_AndMakeCreateTransction()
        {

            Bigchain_MakeCreateTransaction transaction = new Bigchain_MakeCreateTransaction();
            var assets = new Asset
            {
                Assets = new AssetDefinition
                {
                    Data = new DataDefinition
                    {
                        Kyc = new KycDefinition
                        {
                            Dob = "",
                            Nab = "",
                            Pob = "",
                            UserHash = ""
                        }
                    }
                }
            };

            var metadata = new Metadata
            {
                Error = "",
                Status = "",
                Transaction = ""
            };

            TxTemplate txTemplate = transaction.MakeCreateTransaction(assets,
                metadata,
                transaction.MakeOutput(transaction.MakeEd25519Condition(generatedKeyPair.PublicKey)),
                new List<string> { generatedKeyPair.PublicKey }
                );

            Assert.AreEqual(generatedKeyPair.PublicKey, txTemplate.Outputs[0].PublicKeys[0]);
            Assert.AreEqual(generatedKeyPair.PublicKey, txTemplate.Inputs[0].Owners_before[0]);
        }

        [Test, Order(2)]
        public async Task ProvidedSignedTx_ShouldPostCommitTransaction() {
            var signedTx = GetMockResponseSignedTx();
            
            var connection = new BigchainConnection(bigchainhost);
            var (response,status) = await connection.PostTransactionCommit(signedTx);

            Assert.AreNotEqual(status, HttpStatusCode.BadRequest);
            Assert.That(status == HttpStatusCode.Accepted || status == HttpStatusCode.Created || status == HttpStatusCode.NoContent);
        }


        private SignedTxResponse GetMockResponseSignedTx() {
            return new SignedTxResponse
            {
                id = "d48b333ea27d60dae01546a3a184d532e7fad7c7545335ac7d0a32b0fe517a71",
                asset =  new AssetDefinition {
                        Data = new DataDefinition {
                            Kyc = new KycDefinition {
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
                        Condition = new MakeEd25519Condition{
                            Details = new Details{
                                PublicKey = "EN6jFN4LAaBnzkZQekdzYU5XUTyKKX5EiUUBnFgfkozQ",
                                Type = "ed25519-sha-256"
                            },
                             Uri = "ni:///sha-256;uNxDIG7YMPY7EaAVuF_iyn15sxDLeIEzlox7UQOAdmI?fpt=ed25519-sha-256&cost=131072"

                        },
                        PublicKeys = new List<string>(){
                            "9GrgtiScWLhZGT4vk9J1rVy2mr7PCg6ZbNhVYR8aPz6a"
                        }
                    }
                }
            };
        }

    }
}
