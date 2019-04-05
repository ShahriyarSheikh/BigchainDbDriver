using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class Ed25519Condition
    {
        [JsonProperty("details")]
        public Details Details { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Details {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
