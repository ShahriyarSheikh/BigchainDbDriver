using Newtonsoft.Json;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class AssetDefinition
    {
        [JsonProperty("data")]
        public dynamic Assets { get; set; }
    }
}
