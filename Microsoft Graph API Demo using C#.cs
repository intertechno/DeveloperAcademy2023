using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft365GraphApiDemo;

Console.WriteLine("Starting to access Microsoft Graph API.");

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
GraphServiceClient graphClient = new(accessTokenProvider);

UserCollectionResponse? users = await graphClient.Users.GetAsync();

if (users?.Value is not null)
{
    Console.WriteLine("Got the users!");
    string userToSelect = "Megan Bowen";
    string userIdToQuery = "";

    foreach (var user in users.Value)
    {
        Console.WriteLine(user.Id + " " + user.DisplayName);
        if (user.DisplayName == userToSelect)
        {
            userIdToQuery = user.Id;
        }
    }
}

Console.WriteLine("------");
Console.WriteLine("End.");
