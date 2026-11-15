using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json.Linq;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XrmLocalTest
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var serialiserOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var authFlowType = "devicecode";
            string token;

            var impersonatedUpn = "matthew.thomas@stpauls.nsw.edu.au";
            var testObjectId = "d2d1b993-5e14-4fe4-8e42-5810816b589d";

            var configManager = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            string? _clientId = configManager["ClientId"];
            string? _clientSecret = configManager["ClientSecret"];
            string? _instanceUri = configManager["InstanceUri"];
            string? _tenantId = configManager["TenantId"];

            var app = PublicClientApplicationBuilder.Create(_clientId).WithTenantId(_tenantId).Build();

            string connectionString = $@"
                AuthType=ClientSecret;
                Url={_instanceUri};
                ClientId={_clientId};
                ClientSecret={_clientSecret};
            ";

            var serviceClient = new ServiceClient(connectionString);
            var serviceProvider = new MockServiceProvider();

            if (serviceClient.IsReady)
            {
                Console.WriteLine("Connection attempt successful.");

                var whoAmI = (WhoAmIResponse)serviceClient.Execute(
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
            EntityCollection testCollection = XrmHelper.GetMultipleRecords<EntityCollection>(
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

            XrmHelper.UpdateTargetAttributes(
                "mthomas_coreuser",
                testReference,
                testDictionary,
                serviceClient
            );

            EntityCollection testCollectionAfterUpdate = XrmHelper.GetMultipleRecords<EntityCollection>(
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

            Console.WriteLine($"Updated Staff ID: {testCollectionAfterUpdate.Entities[0].GetAttributeValue<int>("mthomas_staffid")}");

            ITracingService? tracingService = (ITracingService?)serviceProvider?.GetService(typeof(ITracingService));

            if (authFlowType != "devicecode")
            {
                if (
                    string.IsNullOrEmpty(_clientId) ||
                    string.IsNullOrEmpty(_clientSecret) ||
                    string.IsNullOrEmpty(_instanceUri) ||
                    string.IsNullOrEmpty(_tenantId)
                )
                {
                    tracingService?.Trace("One or more configuration values are missing. Please check your user secrets.");
                    throw new InvalidPluginExecutionException("One or more configuration values are missing. Please check your user secrets.");
                }
                else
                {
                    var parameterList = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _clientId ?? ""),
                    new KeyValuePair<string, string>("client_secret", _clientSecret ?? ""),
                    new KeyValuePair<string, string>("scope", $"https://graph.microsoft.com/.default")
                };

                    token = XrmHelper.RetrieveBearerToken(
                        tracingService,
                        5,
                        parameterList,
                        _tenantId ?? ""
                    );
                }
            }
            else
            {
                token = await XrmHelper.RetrieveBearerToken(tracingService, app);
                Console.WriteLine($"RETRIEVED DEVICE TOKEN: {token ?? "none"}");
            }

            Console.WriteLine($"Retrieved token: {token ?? "none"}");

            var objectId = XrmHelper.GetUserAzureId(tracingService, impersonatedUpn, testObjectId, token ?? "", false);


            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();

            serviceClient.Dispose();
        }
    }

    public class MockServiceProvider : IServiceProvider
    {
        private readonly ITracingService? _tracingService;
        private readonly IPluginExecutionContext? _pluginExecutionContext;
        private readonly IOrganizationServiceFactory? _organizationServiceFactory;

        public MockServiceProvider(
            ITracingService? tracingService = null,
            IPluginExecutionContext? pluginExecutionContext = null,
            IOrganizationServiceFactory? organizationServiceFactory = null
        )
        {
            _tracingService = tracingService;
            _pluginExecutionContext = pluginExecutionContext;
            _organizationServiceFactory = organizationServiceFactory;
        }

        public object? GetService(Type serviceType)
        {
            if (serviceType == typeof(ITracingService))
            {
                return _tracingService ?? null;
            }
            if (serviceType == typeof(IPluginExecutionContext))
            {
                return _pluginExecutionContext ?? null;
            }
            if (serviceType == typeof(IOrganizationServiceFactory))
            {
                return _organizationServiceFactory ?? null;
            }

            return null;
        }
    }

}