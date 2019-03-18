using BigchainDbDriver.Assets.Models.TransactionModels;
using BigchainDbDriver.Common;
using BigchainDbDriver.Common.Cryptography;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BigchainDbDriver.Transactions
{
    public class Bigchain_SignTransaction
    {
        private readonly DataEncoder encoder;
        public Bigchain_SignTransaction()
        {
            encoder = Encoders.Base58;
        }
        public TxTemplate SignTransaction(TxTemplate transaction, List<string>  privateKeys) {

            //const signedTx = clone(transaction)
            //const serializedTransaction =
            //    Transaction.serializeTransactionIntoCanonicalString(transaction)
            var signedTx = (TxTemplate)transaction.Clone();
            var serializedTransaction = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(transaction));
            var sha256 = new Sha256();

            //signedTx.inputs.forEach((input, index) => {
            //    const privateKey = privateKeys[index]
            //    const privateKeyBuffer = Buffer.from(base58.decode(privateKey))

            //    const transactionUniqueFulfillment = input.fulfills ? serializedTransaction
            //        .concat(input.fulfills.transaction_id)
            //        .concat(input.fulfills.output_index) : serializedTransaction
            //    const transactionHash = sha256Hash(transactionUniqueFulfillment)
            //    const ed25519Fulfillment = new cc.Ed25519Sha256()
            //    ed25519Fulfillment.sign(Buffer.from(transactionHash, 'hex'), privateKeyBuffer)
            //    const fulfillmentUri = ed25519Fulfillment.serializeUri()

            //    input.fulfillment = fulfillmentUri
            //})


            foreach (var privKey in privateKeys)
            {
                var currentPrivKey = privKey;
                var privKeyBuffer = encoder.DecodeData(currentPrivKey);
                var pubKeyBuffer = encoder.DecodeData(transaction.Inputs[0].Owners_before[0]);
                var transactionUniqueFulfillment = transaction.Inputs[0].Fulfills == null ? serializedTransaction : serializedTransaction;

                var transactionHash = sha256.SHA256HexHashString(transactionUniqueFulfillment);
                var signedFulfillment = CryptographyUtility.Ed25519Sign(pubKeyBuffer, privKeyBuffer);
                var fulfillmentUri = CryptographyUtility.SerializeUri(signedFulfillment);
                signedTx.Inputs[0].Fulfillment = fulfillmentUri;
            }


            //const serializedSignedTransaction =
            //    Transaction.serializeTransactionIntoCanonicalString(signedTx)
            //signedTx.id = sha256Hash(serializedSignedTransaction)
            //return signedTx

            var serializedSignedTransaction = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(signedTx));
            signedTx.Id = sha256.SHA256HexHashString(serializedSignedTransaction);
            return signedTx;

        }
    }
}
