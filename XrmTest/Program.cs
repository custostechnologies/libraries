using System;
using spgsWebTools.XrmHelpers;
using System.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Microsoft.Xrm.Sdk;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace XrmLocalTest
{
    public class Program
    {

        static void Main(string[] args)
        {
            IConfigurationRoot _config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            var serialiserOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

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

            // test GetMultipleRecords
            /*EntityCollection testCollection = spgsWebTools.XrmHelpers.GetMultipleRecords<EntityCollection>(
                "mthomas_coreuser",
                "Matthew Thomas",
                "mthomas_userdisplayname",
                ["mthomas_userdisplayname", "mthomas_staffid"],
                1,
                1,
                "",
                false,
                serviceClient
            );
            Console.WriteLine($"Number of records found: {testCollection.TotalRecordCount}");
            foreach (Entity entity in testCollection.Entities)
            {
                Console.WriteLine($"UDN: {entity.GetAttributeValue<string>("mthomas_userdisplayname")}");
            }
            Console.WriteLine($"Serialised JSON: {JsonSerializer.Serialize(testCollection.Entities, serialiserOptions)}");

            Console.WriteLine($"Existing Staff ID: {testCollection.Entities[0].GetAttributeValue<int>("mthomas_staffid")}");

            Guid coreUserGuid = testCollection.Entities[0].Id;

            // test UpdateTargetAttributes

            // instantiate test dictionary
            Dictionary<string, object> testDictionary = new Dictionary<string, object>
            {
                { "mthomas_staffid", 283224 }
            };

            EntityReference testReference = testCollection.Entities[0].ToEntityReference();

            XrmHelpers.UpdateTargetAttributes(
                "mthomas_coreuser",
                testReference,
                testDictionary,
                serviceClient
            );

            EntityCollection testCollectionAfterUpdate = XrmHelpers.GetMultipleRecords<EntityCollection>(
                "mthomas_coreuser",
                "Matthew Thomas",
                "mthomas_userdisplayname",
                ["mthomas_userdisplayname", "mthomas_staffid"],
                1,
                1,
                "",
                false,
                serviceClient
            );

            Console.WriteLine($"Updated Staff ID: {testCollectionAfterUpdate.Entities[0].GetAttributeValue<int>("mthomas_staffid")}");*/

            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
            serviceClient.Dispose();
        }
    }
}