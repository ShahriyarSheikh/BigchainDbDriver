using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class SignedTxResponse
    {
        public string id { get; set; }
        public string operation { get; set; }
        public List<Output> outputs { get; set; }
        public List<InputTemplate> inputs { get; set; }
        public Metadata metadata { get; set; }
        public AssetDefinition asset { get; set; }
        public string version { get; set; }
    }
}
