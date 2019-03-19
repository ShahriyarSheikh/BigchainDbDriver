using BigchainDbDriver.Assets.Models.TransactionModels;
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
        private readonly DataEncoder encoder;
        public Bigchain_SignTransaction()
        {
            encoder = Encoders.Base58;
        }
        public TxTemplate SignTransaction(TxTemplate transaction, List<string>  privateKeys) {

            //Reference#1
            //const signedTx = clone(transaction)
            //const serializedTransaction =
            //    Transaction.serializeTransactionIntoCanonicalString(transaction)
            var signedTx = (TxTemplate)transaction.Clone();
            var serializedTransaction = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(transaction));
            var sha3 = new Sha3_256();

            //Reference#2
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

            var index = 0;
            foreach (var privKey in privateKeys)
            {
                var currentPrivKey = privKey;
                var privKeyBuffer = encoder.DecodeData(currentPrivKey);
                var pubKeyBuffer = encoder.DecodeData(transaction.Inputs[index].Owners_before[0]);
                var transactionUniqueFulfillment = transaction.Inputs[index].Fulfills == null ? serializedTransaction + 
                                                                                                transaction.Inputs[index].Fulfills?.TransactionId + 
                                                                                                transaction.Inputs[index].Fulfills?.OutputIndex : serializedTransaction;

                var transactionHash = sha3.ComputeHash(Encoding.ASCII.GetBytes(transactionUniqueFulfillment));

                var signedFulfillment = CryptographyUtility.Ed25519Sign(pubKeyBuffer, privKeyBuffer);

                var fulfillmentUri = CryptographyUtility.SerializeUri(signedFulfillment);
                signedTx.Inputs[index].Fulfillment = fulfillmentUri;
                index++;
            }

            //Reference#3
            //const serializedSignedTransaction =
            //    Transaction.serializeTransactionIntoCanonicalString(signedTx)
            //signedTx.id = sha256Hash(serializedSignedTransaction)
            //return signedTx

            var serializedSignedTransaction = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(signedTx));
            signedTx.Id = sha3.ComputeHash(Encoding.ASCII.GetBytes(serializedSignedTransaction));
            return signedTx;

        }
    }
}
