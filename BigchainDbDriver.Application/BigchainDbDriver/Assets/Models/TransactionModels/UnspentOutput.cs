using BigchainDbDriver.Transactions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class UnspentOutput
    {
        [JsonProperty("tx")]
        public TxTemplate Tx { get; set; }

        [JsonProperty("output_index")]
        public int OutputIndex { get; set; }
    }
}
