using BigchainDbDriver.Assets.Models.TransactionModels;
using System.Collections.Generic;

namespace BigchainDbDriver.Transactions
{
    public interface IBigchain_SignTransaction
    {

        /// <summary>
        /// Will generate fulfillment Uri based on pubKey(byte[]) and signature
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        string GenerateFulfillmentUri(byte[] publicKey, byte[] signature);

        /// <summary>
        /// Will generate fulfillemnt Uri based on pubKey(string) and signature
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        string GenerateFulfillmentUri(string publicKey, byte[] signature);

        /// <summary>
        /// Will generate signature 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="serializedTransaction"></param>
        /// <param name="index"></param>
        /// <param name="privKey"></param>
        /// <returns></returns>
        SignatureMetadata GetSignature(TxTemplate transaction, string serializedTransaction, int index, string privKey);


        /// <summary>
        /// Will sign a transaction and generate fullfillemntUri for post commit to the network
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="privateKeys"></param>
        /// <returns></returns>
        TxTemplate SignTransaction(TxTemplate transaction, List<string> privateKeys);
    }
}