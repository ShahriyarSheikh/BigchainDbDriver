using BigchainDbDriver.Assets.Models;
using BigchainDbDriver.Assets.Models.TransactionModels;
using BigchainDbDriver.Common;
using BigchainDbDriver.Common.Cryptography;
using BigchainDbDriver.General;
using BigchainDbDriver.KeyPair;
using BigchainDbDriver.Transactions;
using Newtonsoft.Json;
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
        public void ProvidedInput_Payload_Metadata_Keys_AndMakeCreateTransction()
        {

            Bigchain_Transaction transaction = new Bigchain_Transaction();
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

        [Test]
        public void ProvidedPubKey_ShouldGeneratedValidCcUrl() {
            var pubKey = "WuD9VBm3kAUKkZ2Cvvij4QsfkGFqxvfX6qGg6qQxsZs";
            var expectedUri = "dCQ-qJBCsSNC6AGifLWu0Cuhv38V707Tk0C8TdR-R1k";

            var generatedUri = pubKey.EncodeToBase64Url();

            Assert.AreEqual(expectedUri, generatedUri);
        }

        [Test, Order(2)]
        public async Task ProvidedSignedTx_ShouldPostCommitTransaction() {
            var signedTx = GetMockResponseSignedTx();
            
            var connection = new BigchainConnection(bigchainhost);
            var (response,status) = await connection.PostTransactionCommit(signedTx);

            Assert.AreNotEqual(status, HttpStatusCode.BadRequest);
            Assert.That(status == HttpStatusCode.Accepted || status == HttpStatusCode.Created || status == HttpStatusCode.NoContent);
        }

        [Test]
        public void ProvidedTx_ShouldReturnValidSignedTx() {
            var keypairgenerator = new Ed25519Keypair();
            var keypair = keypairgenerator.GenerateKeyPair(new byte[32]);
            var expectedHash = "0b876b6a1604f6f313e63640d6f90eb09d85d56c2036034bc7dbf039cf585f33";

            var tx = GetMockResponseTx(keypair.PublicKey);
            var signTx = new Bigchain_SignTransaction();
            var signedTx = signTx.SignTransaction(tx, new List<string> { $"{keypair.ExpandedPrivateKey}" });
            var serializedTx = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(signedTx));

            Assert.AreEqual(expectedHash, signedTx.Id);

        }

        [Test]
        public void ProvidedString_ShouldReturnValidSha256Hash() {
            var stringToHash = "abc";
            var expectedHash = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad";

            var sha = new Sha256();

            var actualHash = sha.SHA256HexHashString(stringToHash);

            Assert.AreEqual(expectedHash, actualHash);
        }

        [Test]
        public void ProvidedString_ShouldReturnValidSha3256() {
            var stringToHash = "abc";
            var expectedHash = "3a985da74fe225b2045c172d6bd390bd855f086e3e9d525b46bfe24511431532";

            var sha3 = new Sha3_256();

            var actualHash = sha3.ComputeHash(stringToHash);
            Assert.AreEqual(expectedHash, actualHash);
        }


        private TxTemplate GetMockResponseTx(string pubKey)
        {
            return new TxTemplate
            {
                Id = null,
                Asset = new AssetDefinition
                {
                    Data = new DataDefinition
                    {
                        Kyc = new KycDefinition
                        {
                            Dob = "11/23/1995 12:00:00 AM +00:00",
                            Nab = "JohnDoe3",
                            Pob = "PK",
                            UserHash = "5c8655be88dbd41fdc9ed307"
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
                        Condition = new MakeEd25519Condition{
                            Details = new Details{
                                PublicKey = pubKey,
                                Type = "ed25519-sha-256"
                            },
                             Uri = pubKey.GenerateMockUri()

                        },
                        PublicKeys = new List<string>(){
                            pubKey
                        }
                    }
                }
            };
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
                            "EN6jFN4LAaBnzkZQekdzYU5XUTyKKX5EiUUBnFgfkozQ"
                        }
                    }
                }
            };
        }

    }
}
