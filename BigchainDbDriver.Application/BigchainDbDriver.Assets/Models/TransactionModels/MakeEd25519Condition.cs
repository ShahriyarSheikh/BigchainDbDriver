using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class MakeEd25519Condition
    {
        public Details Details { get; set; }
        public string Uri { get; set; }
    }

    public class Details {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
