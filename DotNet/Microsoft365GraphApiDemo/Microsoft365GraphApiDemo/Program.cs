// registration: https://login.microsoftonline.com/02821f79-0170-4f60-8b05-7c868a62280d/adminconsent?client_id=b39415d1-a94d-4da8-a10d-71b50cf5143e

// using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft365GraphApiDemo;

Console.WriteLine("Starting to access Microsoft Graph API.");

/*
IConfigurationRoot config = LoadAppSettings();
string? clientId = config["clientId"];
string? clientSecret = config["clientSecret"];
string? tenantId = config["tenantId"];
*/
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
    string userToSelect = "Jani Järvinen";
    string userIdToQuery = "";

    foreach (var user in users.Value)
    {
        Console.WriteLine(user.Id + " " + user.DisplayName);
        if (user.DisplayName == userToSelect)
        {
            userIdToQuery = user.Id;
        }
    }

    if (userIdToQuery != "")
    {
        Console.WriteLine("------");

        EventCollectionResponse? events = await graphClient.Users[userIdToQuery].Events.GetAsync();
        if (events?.Value is not null)
        {
            foreach (Event @event in events.Value)
            {
                string start = @event.Start.ToDateTimeOffset().ToLocalTime().ToString();
                Console.WriteLine($"{@event.Subject}: {start}");

                // try to modify the event
                @event.Subject = "Updated subject";
                await graphClient.Users[userIdToQuery].Events[@event.Id].PatchAsync(@event);
            }
        }
    }
}

Console.WriteLine("------");
Console.WriteLine("End.");

/*Microsoft.Graph.Models.User? me = await graphClient.Me.GetAsync();
Console.WriteLine($"Hello, {me.DisplayName}!");
*/
/*
static IConfigurationRoot LoadAppSettings()
{
    IConfigurationRoot appConfig = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    if (string.IsNullOrEmpty(appConfig["clientId"]) ||
        string.IsNullOrEmpty(appConfig["clientSecret"]) ||
        string.IsNullOrEmpty(appConfig["tenantId"]))
    {
        throw new InvalidOperationException("Missing app settings");
    }

    return appConfig;
}
*/