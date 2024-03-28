using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions.Authentication;

namespace Microsoft365GraphApiDemoWithWebUserInterface
{
    public class GraphApiHelper
    {
        protected GraphServiceClient GraphClient { get; set; }

        public async Task AuthenticateAsync()
        {
            string clientId = "b39415d1-a94d-4da8-a10d-71b50cf5143e";
            string clientSecret = "XXXXXXX";
            string tenantId = "02821f79-0170-4f60-8b05-7c868a62280d";
            string[] scopes = ["https://graph.microsoft.com/.default"];

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}/v2.0"))
                .Build();

            AuthenticationResult authResult = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            string accessToken = authResult.AccessToken;
            BaseBearerTokenAuthenticationProvider accessTokenProvider = new(new TokenProvider(accessToken));
            GraphClient = new(accessTokenProvider);
        }

        public async Task<Dictionary<string, string>> GetUsersAsync()
        {
            UserCollectionResponse? users = await GraphClient.Users.GetAsync();
            Dictionary<string, string> usersDictionary = new();

            if (users != null)
            {
                foreach(User user in users.Value)
                {
                    usersDictionary.Add(user.Id, user.DisplayName);
                }
            }

            return usersDictionary;
        }

        public async Task<Dictionary<string, string>> GetCalendarEventsForUserAsync(string userId)
        {
            EventCollectionResponse? response = await GraphClient.Users[userId].Calendar.Events.GetAsync();
            Dictionary<string, string> eventsDictionary = [];

            if (response != null)
            {
                foreach(Event calendarEvent in response.Value)
                {
                    eventsDictionary.Add(calendarEvent.Subject, calendarEvent.Start.DateTime.ToString());
                }
            }

            return eventsDictionary;
        }
    }
}
