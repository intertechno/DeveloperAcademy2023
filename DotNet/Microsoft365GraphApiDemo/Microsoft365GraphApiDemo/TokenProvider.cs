using Microsoft.Kiota.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft365GraphApiDemo
{
    public class TokenProvider : IAccessTokenProvider
    {
        public AllowedHostsValidator AllowedHostsValidator { get; }
        public string Token { get; }

        public TokenProvider(string token)
        {
            Token = token;
        }

        public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = default,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Token);
        }
    }
}
