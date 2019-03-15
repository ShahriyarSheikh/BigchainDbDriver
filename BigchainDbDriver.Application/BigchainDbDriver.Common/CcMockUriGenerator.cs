using BigchainDbDriver.Common.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Common
{
    public static class CcMockUriGenerator
    {
        public static string GenerateMockUri(this string pubKey) {
            var baseUri = "ni:///sha-256;";
            var queryParams = "?fpt=ed25519-sha-256&cost=131072";
            return $"{baseUri}{pubKey.EncodeToBase64Url()}{queryParams}";
        }
    }
}
