using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class Fulfill
    {
        [JsonProperty("output_index")]
        public string OutputIndex { get; set; }
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
    }
}
