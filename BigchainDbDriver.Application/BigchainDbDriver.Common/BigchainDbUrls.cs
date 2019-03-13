using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Common
{
    public static class BigchainDbUrls
    {
        public static string Blocks { get { return "blocks"; } }
        public static string BlocksDetails { get { return "blocks/%(blockHeight)s"; } }
        public static string Outputs { get { return "outputs"; } }
        public static string Transactions { get { return "transactions"; } }
        public static string TransactionsSync { get { return "transactions?mode=sync"; } }
        public static string TransactionsAsync { get { return "transactions?mode=async"; } }
        public static string TransactionsCommit { get { return "transactions?mode=commit"; } }
        public static string TransactionsDetail { get { return "transactions/%(transactionId)s"; } }
        public static string Assets { get { return "assets"; } }
        public static string Metadata { get { return "metadata"; } }
        public static string Votes { get { return "votes"; } }

        public static Dictionary<string, string> BigchainDbURLs = new Dictionary<string, string>(){
            { "blocks", "blocks" },
            { "blocksDetail", "blocks/%(blockHeight)s" },
            { "outputs", "outputs" },
            { "transactions", "transactions" },
            { "transactionsSync", "transactions?mode=sync" },
            { "transactionsAsync", "transactions?mode=async" },
            { "transactionsCommit", "transactions?mode=commit" },
            { "transactionsDetail", "transactions/%(transactionId)s" },
            { "assets", "assets" },
            {"metadata","metadata" },
            { "votes","votes"}
        };
    }
}
