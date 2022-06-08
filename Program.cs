using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using System.Collections.Generic;

namespace az204_authdemo
{
    class Program
    {
        private const string _clientId = "______342-83c9-4838-a948-9499______";
        private const string _tenantId = "______004-8b43-4d2f-ba4b-ca2bd______";
        public static async Task Main(string[] args)
        {
            //provide login
            var app = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
            .WithRedirectUri("http://localhost")
            .Build();
            
            Console.WriteLine("press enter to login...");
            Console.ReadLine();

            //get user info
            string[] scopes = { "user.read" };
            AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
            Console.WriteLine($"Token:\t{result.AccessToken}");

            Console.WriteLine("press enter to get user info...");
            Console.ReadLine();

            var provider = new InteractiveAuthenticationProvider(app, scopes);
            var client = new GraphServiceClient(provider);
            User me = await client.Me.Request().GetAsync();
            Console.WriteLine($"Display Name:\t{me.DisplayName}");

            Console.WriteLine("press enter to exit...");
            Console.ReadLine();
        }
    }
}
