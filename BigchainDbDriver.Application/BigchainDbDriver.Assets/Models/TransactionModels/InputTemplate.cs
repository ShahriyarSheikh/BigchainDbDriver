using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class InputTemplate
    {
        public string Fulfilment { get; set; }
        public string Fulfills { get; set; }
        [JsonProperty("owners_before")]
        public List<string> Owners_before { get; set; }
    }
}
