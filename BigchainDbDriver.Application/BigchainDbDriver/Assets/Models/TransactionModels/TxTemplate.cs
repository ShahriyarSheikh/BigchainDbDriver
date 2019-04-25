using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class TxTemplate : ICloneable
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("operation")]
        public dynamic Operation { get; set; }
        [JsonProperty("outputs")]
        public List<Output> Outputs { get; set; }
        [JsonProperty("inputs")]
        public List<InputTemplate> Inputs { get; set; }
        [JsonProperty("metadata")]
        public dynamic Metadata { get; set; }
        [JsonProperty("asset")]
        public dynamic Asset { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
