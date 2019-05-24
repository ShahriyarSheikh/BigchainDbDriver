using BigchainDbDriver.Assets.Models.ResponseModels;
using BigchainDbDriver.Assets.Models.TransactionModels;
using System.Net;
using System.Threading.Tasks;

namespace BigchainDbDriver.Client
{
    public interface IBigchainConnectionWrite : IBigchainConnectionRead
    {


        /// <summary>
        /// Commits a signed transaction to bigchain db
        /// </summary>
        /// <param name="transaction">Signed transaction with valid fulfills and fulfillmentUri</param>
        /// <returns></returns>
        Task<(SignedTxResponse, HttpStatusCode)> PostTransactionCommit(SignedTxResponse transaction);

    }

    public interface IBigchainConnectionRead {

        Task<dynamic> SearchAssets(dynamic search);
        Task<dynamic> SearchMetadata(dynamic search);

        /// <summary>
        /// Provided blockheight, returns block details
        /// </summary>
        /// <param name="blockHeight">Height of block</param>
        /// <returns>Block details</returns>
        Task<Block> GetBlock(string blockHeight);
        Task<dynamic> GetTransaciton(string transactionId);

        /// <summary>
        /// Provided transaction id of block (i.e. hash) should return block details
        /// </summary>
        /// <param name="transctionId">Transaction id (i.e. transaction hash)</param>
        /// <returns></returns>
        Task<Block> ListBlocks(string transctionId);
        Task<dynamic> ListOutputs(string publicKey, string spent);
        Task<dynamic> ListTransactions(string assetId, string operation);
        Task<dynamic> ListVotes(dynamic blockId);
    }
}