using BigchainDbDriver.KeyPair;
using BigchainDbDriver.Transactions;
using System;
using System.Transactions;

namespace BigchainDbDriver.Application
{
	class Program
	{
		static void Main(string[] args)
		{
            Ed25519Keypair ed25519Keypair = new Ed25519Keypair();
            
            var trans = new Bigchain_MakeCreateTransaction();
            var res = trans.MakeEd25519Condition("EsyuLTCMqB4aHBXBC4UAJhAxxRQF9Trbmx2hjFPanZvL");

            var result = ed25519Keypair.GenerateKeyPair(new byte[32]);
		}
	}
}
