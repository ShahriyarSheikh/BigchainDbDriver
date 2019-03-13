using System;

namespace BigchainDbDriver.General
{
    public interface IBigchainConnection
    {
        Uri GetBlock(string blockHeight);
        Uri GetTransaciton(string transactionId);
        Uri ListBlocks(string transctionId);
        Uri ListOutputs(string publicKey, string spent);
        Uri ListTransactions(string assetId, string operation);
        Uri ListVotes(string blockId);
        Uri PostTransaction(string transaction);
        Uri PostTransactionSync(string transaction);
        Uri PostTransactionAsync(string transaction);
        Uri PostTransactionCommit(string transaction);
        Uri SearchAssets(string search);
        Uri searchMetadata(string search);
    }
}