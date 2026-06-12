using System;
using spgsWebTools.XrmHelpers;
using System.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Crm.Sdk.Messages;

namespace XrmLocalTest
{
    public class Program
    {
        static readonly string? _clientId = ConfigurationManager.AppSettings["ClientId"];
        static readonly string? _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        static readonly string? _instanceUri = ConfigurationManager.AppSettings["InstanceUri"];

        static void Main(string[] args)
        {
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
            Console.ReadLine();
            serviceClient.Dispose();
        }
    }
}