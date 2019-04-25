using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Assets.Models.TransactionModels
{
    public class SignatureMetadata
    {
        public byte[] TransactionHash { get; set; }
        public byte[] PubKeyBuffer { get; set; }
        public byte[] Signature { get; set; }
    }
}
