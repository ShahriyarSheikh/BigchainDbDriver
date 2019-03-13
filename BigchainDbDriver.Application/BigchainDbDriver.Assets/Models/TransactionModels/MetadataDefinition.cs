using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class Metadata
    {
        public string Status { get; set; }
        public string Transaction { get; set; }
        public string Error { get; set; }
    }
}
