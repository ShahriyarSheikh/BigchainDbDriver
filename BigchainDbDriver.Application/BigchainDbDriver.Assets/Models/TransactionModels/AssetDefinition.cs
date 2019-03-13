using Newtonsoft.Json;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class Asset {
        [JsonProperty("asset")]
        public AssetDefinition Assets { get; set; }
    }
    public class AssetDefinition
    {
        [JsonProperty("data")]
        public DataDefinition Data { get; set; }
    }

    public class DataDefinition {
        [JsonProperty("kyc")]
        public KycDefinition Kyc { get; set; }
    }

    public class KycDefinition
    {
        [JsonProperty("pob")]
        public string Pob { get; set; }
        [JsonProperty("dob")]
        public string Dob { get; set; }
        [JsonProperty("nab")]
        public string Nab { get; set; }
        [JsonProperty("user_hash")]
        public string UserHash { get; set; }

    }
}
