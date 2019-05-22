using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class InputTemplate
    {
        [JsonProperty("fulfillment")]
        public string Fulfillment { get; set; }
        [JsonProperty("fulfills")]
        public Fulfill Fulfills { get; set; }
        [JsonProperty("owners_before")]
        public IList<string> Owners_before { get; set; }
    }
}
