using BigchainDbDriver.Assets.Models.ResponseModels;
using BigchainDbDriver.Assets.Models.TransactionModels;
using System.Net;
using System.Threading.Tasks;

namespace BigchainDbDriver.General
{
    public interface IBigchainConnection
    {
        Task<Block> GetBlock(string blockHeight);
        Task<dynamic> GetTransaciton(string transactionId);
        Task<Block> ListBlocks(string transctionId);
        Task<dynamic> ListOutputs(string publicKey, string spent);
        Task<dynamic> ListTransactions(string assetId, string operation);
        Task<dynamic> ListVotes(dynamic blockId);
        Task<(SignedTxResponse, HttpStatusCode)> PostTransactionCommit(SignedTxResponse transaction);
        Task<dynamic> SearchAssets(dynamic search);
        Task<dynamic> SearchMetadata(dynamic search);
    }
}