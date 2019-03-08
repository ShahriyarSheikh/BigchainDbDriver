using BigchainDbDriver.KeyPair;
using System;

namespace BigchainDbDriver.Application
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
            Ed25519Keypair ed25519Keypair = new Ed25519Keypair();
            var result = ed25519Keypair.GenerateKeyPair("abc");
		}
	}
}
