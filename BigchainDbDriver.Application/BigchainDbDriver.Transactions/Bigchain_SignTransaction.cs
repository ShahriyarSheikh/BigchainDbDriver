using BigchainDbDriver.Common;
using BigchainDbDriver.Common.Cryptography;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Transactions
{
    public class Bigchain_SignTransaction
    {
        public TxTemplate SignTransaction(TxTemplate transaction, List<string> privateKeys)
        {
            var signedTx = (TxTemplate)transaction.Clone();

            var serializedTransaction = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(transaction));

            var index = 0;
            foreach (var privKey in privateKeys)
            {
                (byte[] transactionHash, byte[] pubKeyBuffer, byte[] signature) = GetSignature(transaction, serializedTransaction, index, privKey);

                bool verifyFullfill = signature.VerifySignature(transactionHash, pubKeyBuffer);
                if (!verifyFullfill)
                    continue;


                signedTx.Inputs[index].Fulfillment = GenerateFulfillmentUri(pubKeyBuffer, signature);
                index++;
            }

            var serializedSignedTransaction = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(signedTx));
            signedTx.Id = HashingUtils.ComputeSha3Hash(Encoding.UTF8.GetBytes(serializedSignedTransaction)).ToHex(false);
            return signedTx;
        }

        public (byte[], byte[], byte[]) GetSignature(TxTemplate transaction, string serializedTransaction, int index, string privKey)
        {
            var pubKeyBuffer = Encoders.Base58.DecodeData(transaction.Outputs[index].PublicKeys[0]);

            string transactionUniqueFulfillment = GetUniqueFulfillment(transaction, serializedTransaction, index);

            var transactionHash = HashingUtils.ComputeSha3Hash(Encoding.UTF8.GetBytes(transactionUniqueFulfillment));

            var signature = CryptographyUtility.Ed25519Sign(transactionHash, Encoders.Base58.DecodeData(privKey));

            return (transactionHash, pubKeyBuffer, signature);
        }

        private static string GetUniqueFulfillment(TxTemplate transaction, string serializedTransaction, int index)
        {
            return transaction.Inputs[index].Fulfills == null ?
                                                                serializedTransaction +
                                                                transaction.Inputs[index].Fulfills?.TransactionId +
                                                                transaction.Inputs[index].Fulfills?.OutputIndex : serializedTransaction;
        }

        public string GenerateFulfillmentUri(string publicKey, byte[] signature)
        {
            var pubkeyBytes = Encoders.Base58.DecodeData(publicKey);
            return GenerateFulfillmentUri(pubkeyBytes, signature);
        }

        public string GenerateFulfillmentUri(byte[] publicKey, byte[] signature)
        {
            var asn1 = new Asn1lib(publicKey);
            var serializedUriData = asn1.SerializeBinary(signature);
            var fulfillmentUri = CryptographyUtility.SerializeUri(serializedUriData);
            return fulfillmentUri;
        }
    }
}
