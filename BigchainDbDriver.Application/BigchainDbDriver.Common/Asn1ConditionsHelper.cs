using BigchainDbDriver.Assets.Models.TransactionModels;
using BigchainDbDriver.Common.Cryptography;
using NBitcoin.DataEncoders;

namespace BigchainDbDriver.Common
{
    public class Asn1ConditionsHelper
    {
        public static Ed25519Condition MakeEd25519Condition(string pubkey)
        {
            return new Ed25519Condition
            {
                Details = new Details
                {
                    PublicKey = pubkey,
                    Type = "ed25519-sha-256"
                },
                Uri = GetConditionUri(pubkey)
            };
        }

        private static string GetConditionUri(string pubKey)
        {
            Asn1lib asn1 = new Asn1lib(Encoders.Base58.DecodeData(pubKey));

            var fingerprint = asn1.GetFingerprint();

            var baseUri = "ni:///sha-256;";
            var queryParams = "?fpt=ed25519-sha-256&cost=131072";
            return $"{baseUri}{Base64Url.Encode(fingerprint)}{queryParams}";
        }
    }
}
