
using BigchainDbDriver.Assets.Models;
using BigchainDbDriver.Assets.Models.TransactionModels;
using BigchainDbDriver.Common;
using BigchainDbDriver.Common.Cryptography;
using BigchainDbDriver.KeyPair;
using BigchainDbDriver.Transactions;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace BigchainDbDriver.NUnit.Tests
{
    [TestFixture]
    class Transaction
    {
        private readonly GeneratedKeyPair generatedKeyPair;
       

        private readonly byte[] signature = new byte[] {
             225,57,19,69,249,156,141,95,223,216,153,219,164,203,24,173,250,213,133,186,189,142,
            21,154,36,131,16,253,239,243,51,183,176,186,237,192,225,47,211,254,21,233,77,20,149,
            116,178,177,90,27,25,176,229,244,53,145,22,19,122,103,158,220,254,9
        };

        public Transaction()
        {
            var keypair = new Ed25519Keypair();
            generatedKeyPair = keypair.GenerateKeyPair();
        }

        [TestCase("AU8ynfHa1y9ZM3YjzkVr5TcYDBnDnmhr7UBfUnj5hDYM", "ni:///sha-256;cedzINwbzW20m_uphjne5UusQBwu_VGHwYsehAqiuQ0?fpt=ed25519-sha-256&cost=131072")]
        [TestCase("HpaaTTotR35kdt9HKJfMBKx5FBTH3JLz4gZMqG4Ko4Kg", "ni:///sha-256;jdtM42h066ton_Wzp9HlJ3xLePGaXtjZZxQVRob1Vpg?fpt=ed25519-sha-256&cost=131072")]
        [TestCase("GrnSgK6oaZB8puXjU87eoXUSfgfcGqXg2Y16J6hKZqWR", "ni:///sha-256;nVoPhxLQCOh6XDztTjYRRG2Zwn7cOqTpMZs_0R8DidY?fpt=ed25519-sha-256&cost=131072")]
        public void ProvidedInput_Payload_Metadata_Keys_AndMakeCreateTransction(string pubKey, string fulfill)
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
                transaction.MakeOutput(Asn1ConditionsHelper.MakeEd25519Condition(pubKey)),
                new List<string> { pubKey }
                );

            Assert.AreEqual(pubKey, txTemplate.Outputs[0].PublicKeys[0]);
            Assert.AreEqual(pubKey, txTemplate.Inputs[0].Owners_before[0]);
            Assert.AreEqual(txTemplate.Outputs[0].Condition.Uri,fulfill);
        }

        [Test]
        public void ProvidedPubKey_ShouldGeneratedValidCcUrl()
        {
            var pubKey = "J7rTDMcY48AfV7b18ptpFRnn1un5s7SYENEqsQHHWAps";
            var expectedUri = "IGzUc6y7uenZWKeOWy_TdkgkTkk2pvwXMwNkBGVOFU8";

            byte[] pubKeyBytes = Encoders.Base58.DecodeData(pubKey);

            var asn1 = new Asn1lib(pubKeyBytes);

            string base64UrlEncoded = Base64Url.Encode(asn1.GetFingerprint());

            Assert.AreEqual(expectedUri, base64UrlEncoded);
        }

        [Test]
        public void ProvidedTx_ShouldReturnValidSignedTxString()
        {
            var expectedSerializedTransaction = "{\"asset\":{\"data\":{\"kyc\":{\"dob\":\"7/19/1988 12:00:00 AM +05:00\",\"nab\":\"Hang MioLoi\",\"pob\":\"CN\",\"user_hash\":\"5c9b0ddd16f0d6471c661c0e\"}}},\"id\":null,\"inputs\":[{\"fulfillment\":null,\"fulfills\":null,\"owners_before\":[\"6up1UDJC2EnReHcdYVZkJdR8hbSym48P5XWiyD1oW2Qm\"]}],\"metadata\":{\"Error\":null,\"Status\":\"A\",\"Transaction\":null},\"operation\":\"CREATE\",\"outputs\":[{\"amount\":\"1\",\"condition\":{\"details\":{\"public_key\":\"6up1UDJC2EnReHcdYVZkJdR8hbSym48P5XWiyD1oW2Qm\",\"type\":\"ed25519-sha-256\"},\"uri\":\"ni:///sha-256;dYgl9YZ2-J8GI8zDqGjGCf-iTRt3-N_au_w6QyexL_4?fpt=ed25519-sha-256&cost=131072\"},\"public_keys\":[\"6up1UDJC2EnReHcdYVZkJdR8hbSym48P5XWiyD1oW2Qm\"]}],\"version\":\"2.0\"}";

            var keyPair = new GeneratedKeyPair()
            {
                ExpandedPrivateKey = "2SmR42zKeTExHeXa5gFCJzfVLzPb843bkv9L9EGNxfABo9roeGWuNvTg5KFCcNoA6xd4VnWnX1yzALSvszBFncGu",
                PrivateKey = "5ryt9DgJWu2G5Ptzd5CwUXsvw5DKygd4serdMoJ67KCm",
                PublicKey = "6up1UDJC2EnReHcdYVZkJdR8hbSym48P5XWiyD1oW2Qm"
            };

            var tx = GetMockResponseTx(keyPair.PublicKey);

            var serializedTx = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(tx));

            Assert.AreEqual(expectedSerializedTransaction, serializedTx);
        }

        [Test]
        public void ProvidedTx_ShouldReturnValidSignedTxHash()
        {
            var expectedSerializedTransactionHash = "7c93d8e95c6e2d1b6ba6d77020a9e7d09d30c498fcb0485801fc7507fda43fca";

            var keyPair = new GeneratedKeyPair()
            {
                ExpandedPrivateKey = "2SmR42zKeTExHeXa5gFCJzfVLzPb843bkv9L9EGNxfABo9roeGWuNvTg5KFCcNoA6xd4VnWnX1yzALSvszBFncGu",
                PrivateKey = "5ryt9DgJWu2G5Ptzd5CwUXsvw5DKygd4serdMoJ67KCm",
                PublicKey = "6up1UDJC2EnReHcdYVZkJdR8hbSym48P5XWiyD1oW2Qm"
            };

            var tx = GetMockResponseTx(keyPair.PublicKey);

            var serializedTransaction = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(tx));

            var signTx = new Bigchain_SignTransaction();

            SignatureMetadata signatures = signTx.GetSignature(tx, serializedTransaction, 0, keyPair.ExpandedPrivateKey);

            Assert.AreEqual(expectedSerializedTransactionHash, signatures.TransactionHash.ToHex());
        }

        [Test]
        public void Provided_Payload_Should_Return_ValidSerializedTx()
        {
            var keyPair = new GeneratedKeyPair()
            {
                ExpandedPrivateKey = "2SmR42zKeTExHeXa5gFCJzfVLzPb843bkv9L9EGNxfABo9roeGWuNvTg5KFCcNoA6xd4VnWnX1yzALSvszBFncGu",
                PrivateKey = "5ryt9DgJWu2G5Ptzd5CwUXsvw5DKygd4serdMoJ67KCm",
                PublicKey = "6up1UDJC2EnReHcdYVZkJdR8hbSym48P5XWiyD1oW2Qm"
            };

            var expectedHash = "1a7e241014157ffaca8b854c6775186b581d2058a6299b6d4e79437fce4979fc";

            var tx = GetMockResponseTx(keyPair.PublicKey);

            var signTx = new Bigchain_SignTransaction();
            var signedTx = signTx.SignTransaction(tx, new List<string> { keyPair.ExpandedPrivateKey });
            var serializedTx = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(signedTx));

            Assert.AreEqual(expectedHash, signedTx.Id);
        }

        [Test]
        public void Provided_Fulfillment_Should_Return_DerEncodedFullfillment()
        {
            var expectedFulfillment = "pGSAIOwsDX_8KpzAef-aHlT1QXPnf23YDNEHK26-hw9xtTgEgUDhORNF-ZyNX9_Ymdukyxit-tWFur2OFZokgxD97_Mzt7C67cDhL9P-FelNFJV0srFaGxmw5fQ1kRYTemee3P4J";
            var transactionHash = "28a985bcf3b46a6895035b9f0fb7962190f76316eb46c5a0f3450195200b5780";
            var fulfillment = "ni:///sha-256;NAgseHeCPxu1v5vqPE-mF_IFk6EqBdk7YuAW3LltFAM?fpt=ed25519-sha-256&cost=131072";
            var signedTxId = "f84adc4d2dc630f4f3380b94bd82a196e40907bb55cddb7822842703c789246d";

            var keyPair = new GeneratedKeyPair()
            {
                PrivateKey = "8hiZ8FPQLQnmFqXg8T1L3tgkJvLPeZXnGuThprDDJtQR",
                PublicKey = "GtvBGsnVhGnqR1RswqT3KSwdoU3UW7w23ukmDaH7uAEF"
            };

            var signTx = new Bigchain_SignTransaction();
            var fulfillmentUri = signTx.GenerateFulfillmentUri(keyPair.PublicKey, signature);

            Assert.AreEqual(expectedFulfillment, fulfillmentUri);

        }

        [Test]
        public void ProvidedString_ShouldReturnValidSha256Hash()
        {
            var stringToHash = "abc";
            var expectedHash = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad";

            var actualHash = HashingUtils.SHA256HexHashString(stringToHash);

            Assert.AreEqual(expectedHash, actualHash);
        }

        [Test]
        public void Provided_PublicKey_Should_Return_Valid_Bytes()
        {
            byte[] expectedBytes = new byte[] {
                236,44,13,127,252,42,156,192,121,255,154,30,84,245,65,115,231,127,109,216,12,209,7,43,110,190,135,15,113,181,56,4
            };
            var pubKey = "GtvBGsnVhGnqR1RswqT3KSwdoU3UW7w23ukmDaH7uAEF";

            Assert.AreEqual(expectedBytes, Encoders.Base58.DecodeData(pubKey));

        }

        [Test]
        public void ProvidedString_ShouldReturnValidSha3256()
        {
            var stringToHash = "{\"asset\":{\"data\":{\"kyc\":{\"dob\":\"7/19/1988 12:00:00 AM +05:00\",\"nab\":\"Hang MioLoi\",\"pob\":\"CN\",\"user_hash\":\"5c9b0ddd16f0d6471c661c0e\"}}},\"id\":null,\"inputs\":[{\"fulfillment\":null,\"fulfills\":null,\"owners_before\":[\"GtvBGsnVhGnqR1RswqT3KSwdoU3UW7w23ukmDaH7uAEF\"]}],\"metadata\":{\"Error\":null,\"Status\":\"A\",\"Transaction\":null},\"operation\":\"CREATE\",\"outputs\":[{\"amount\":\"1\",\"condition\":{\"details\":{\"public_key\":\"GtvBGsnVhGnqR1RswqT3KSwdoU3UW7w23ukmDaH7uAEF\",\"type\":\"ed25519-sha-256\"},\"uri\":\"ni:///sha-256;GtvBGsnVhGnqR1RswqT3KSwdoU3UW7w23ukmDaH7uAEF?fpt=ed25519-sha-256&cost=131072\"},\"public_keys\":[\"GtvBGsnVhGnqR1RswqT3KSwdoU3UW7w23ukmDaH7uAEF\"]}],\"version\":\"2.0\"}";
            var expectedHash = "38f4e09c71930ad235bf89f9772845510832a20ae54cfa1a3ab766531b87837a";

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
