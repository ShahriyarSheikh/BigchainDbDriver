using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BigchainDbDriver.Assets.Models.ResponseModels
{


    public class Block
    {
        [JsonProperty("inputs")]
        public List<Input> Inputs { get; set; }
        [JsonProperty("outputs")]
        public List<Output> Outputs { get; set; }
        [JsonProperty("operation")]
        public string Operation { get; set; }
        [JsonProperty("metadata")]
        public JObject Metadata { get; set; }
        [JsonProperty("asset")]
        public Asset Asset { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }


    public class Input
    {
        [JsonProperty("owners_before")]
        public List<string> OwnersBefore { get; set; }
        [JsonProperty("fulfills")]
        public dynamic Fulfills { get; set; }
        [JsonProperty("fulfillment")]
        public string Fulfillment { get; set; }
    }

    public class Details
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
    }

    public class Condition
    {
        [JsonProperty("details")]
        public Details Details { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Output
    {
        [JsonProperty("public_keys")]
        public List<string> PublicKeys { get; set; }
        [JsonProperty("condition")]
        public Condition Condition { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
    }

    public class Asset
    {
        //[JsonProperty("data")]
        //public JObject Data { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }


}
