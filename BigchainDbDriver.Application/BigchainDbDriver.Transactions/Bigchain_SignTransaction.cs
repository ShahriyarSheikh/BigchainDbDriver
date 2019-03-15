using BigchainDbDriver.Common;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;

namespace BigchainDbDriver.Transactions
{
    public class Bigchain_SignTransaction
    {
        private readonly DataEncoder encoder;
        public Bigchain_SignTransaction()
        {
            encoder = Encoders.Base58;
        }
        public void SignTransaction(TxTemplate transaction, params[] privateKeys) {

            //const signedTx = clone(transaction)
            //const serializedTransaction =
            //    Transaction.serializeTransactionIntoCanonicalString(transaction)
            var signedTx = (TxTemplate)transaction.Clone();
            var serializedTransaction = JsonUtility.SerializeTransactionIntoCanonicalString(JsonConvert.SerializeObject(transaction));


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


            //foreach (var privKey in privateKeys) {
            //    var currentPrivKey = privKey;
            //    var privKeyBuffer = encoder.DecodeData(currentPrivKey);
            //    var transactionUniqueFulfillment = transaction.Inputs[0].Fulfills == null ? serializedTransaction : serializedTransaction ;

            //    var transactionHash = 

            //}


            //const serializedSignedTransaction =
            //    Transaction.serializeTransactionIntoCanonicalString(signedTx)
            //signedTx.id = sha256Hash(serializedSignedTransaction)
            //return signedTx
        }
    }
}
