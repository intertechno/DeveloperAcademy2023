using Microsoft.Kiota.Abstractions.Authentication;

namespace Microsoft365GraphApiDemoWithWebUserInterface
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
