using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class AssetLink
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
