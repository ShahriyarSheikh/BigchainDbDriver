using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class Output
    {
        [JsonProperty("amount",Order =1)]
        public string Amount { get; set; }

        [JsonProperty("condition", Order = 2)]
        public MakeEd25519Condition Condition { get; set; }

        [JsonProperty("public_keys", Order = 3)]
        public IList<string> PublicKeys { get; set; }
    }
}
