using System;
using spgsWebTools.XrmHelpers;
using System.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace XrmLocalTest
{
    public class Program
    {

        static void Main(string[] args)
        {
            IConfigurationRoot _config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            string? _clientId = _config["appSettings:ClientId"];
            string? _clientSecret = _config["appSettings:ClientSecret"];
            string? _instanceUri = _config["appSettings:InstanceUri"];

            string connectionString = $@"
                AuthType=ClientSecret;
                Url={_instanceUri};
                ClientId={_clientId};
                ClientSecret={_clientSecret};
            ";

            var serviceClient = new ServiceClient(connectionString);

            if (serviceClient.IsReady)
            {
                Console.WriteLine("Connection attempt successful.");

                var whoAmI = (WhoAmIResponse) serviceClient.Execute(
                    new WhoAmIRequest()
                );

                Console.WriteLine($"User ID: {whoAmI.UserId}");
                Console.WriteLine($"Organisation ID: {whoAmI.OrganizationId}");
            } 
            else
            {
                Console.WriteLine("Connection Failed:");
                Console.WriteLine(serviceClient.LastError);    
            }

            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
            serviceClient.Dispose();
        }
    }
}