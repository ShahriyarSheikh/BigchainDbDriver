using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class Output
    {
        [JsonProperty("condition")]
        public MakeEd25519Condition Condition { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("public_keys")]
        public IList<string> PublicKeys { get; set; }
    }
}
