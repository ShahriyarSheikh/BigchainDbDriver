using System.Collections.Generic;
using BigchainDbDriver.Assets.Models.TransactionModels;

namespace BigchainDbDriver.Transactions
{
    public interface IBigchain_TransactionWrite : IBigchain_TransactionRead
    {
        /// <summary>
        /// Creates a transfer transaction template based on unspendoutputs and outputs received from block details
        /// </summary>
        /// <param name="unspentOutputs">List of outputs that are unspent</param>
        /// <param name="outputs">New outputs needed to transfer</param>
        /// <param name="metadata">Optional data for transfer</param>
        /// <returns></returns>
        TxTemplate MakeTransferTransaction(List<UnspentOutput> unspentOutputs, List<Output> outputs, dynamic metadata);
    }

    public interface IBigchain_TransactionRead {
        /// <summary>
        /// Creates a transaction object to be pushed to bigchaindb
        /// </summary>
        /// <param name="assets"> An object that consists of the definition of data to store in bigchain</param>
        /// <param name="metadata"> Added information for storing in bigchain</param>
        /// <param name="outputs"> Creates output object </param>
        /// <param name="issuers"> Consist of a list of public keys</param>
        /// <returns></returns>
        TxTemplate MakeCreateTransaction(Asset assets, dynamic metadata, List<Output> outputs, List<string> issuers);

        /// <summary>
        /// Creates a list of outputs provided a proper ed25519 condition against an amount
        /// </summary>
        /// <param name="condition"> An Ed25519 condition </param>
        /// <param name="amount">An amount to submit to bigchain, default is 1</param>
        /// <returns></returns>
        List<Output> MakeOutput(Ed25519Condition condition, string amount = "1");

        /// <summary>
        /// Creates an ouput template for a transaction of type 'TRANSFER'
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Output MakeTransferOutput(Ed25519Condition condition);

    }
}