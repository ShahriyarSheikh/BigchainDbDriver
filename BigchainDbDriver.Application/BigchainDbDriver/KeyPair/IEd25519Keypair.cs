using BigchainDbDriver.Assets.Models;

namespace BigchainDbDriver.KeyPair
{
    public interface IEd25519Keypair
    {
        GeneratedKeyPair GenerateKeyPair(byte[] seed = null);
    }
}