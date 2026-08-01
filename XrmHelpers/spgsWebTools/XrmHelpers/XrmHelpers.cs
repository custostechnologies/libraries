using System;
using Microsoft.Identity.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace spgsWebTools;

public class XrmHelpers
{
    public static T? GetValue<T>(string attributeName, Entity target, Entity entity)
    {
        try
        {
            if (target.Contains(attributeName))
            {
                return target.GetAttributeValue<T>(attributeName);
            }

            if (entity != null && entity.Contains(attributeName))
            {
                return entity.GetAttributeValue<T>(attributeName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: {0}, attributeName: {1}", ex, attributeName);
            throw;
        }

        return default;
    }

    /* overloaded helper to retrieve value from one entity */
    public static T? GetValue<T>(string attributeName, Entity entity)
    {
        try
        {
            if (entity != null && entity.Contains(attributeName))
            {
                return entity.GetAttributeValue<T>(attributeName);
            }
            else
            {
                return default;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in GetValue<T>(attributeName, Entity): {0}", ex);
            throw;
        }
    }

    // TO ADD: string array
    public static Microsoft.Xrm.Sdk.EntityCollection GetMultipleRecords<EntityCollection>(
        string entityName,
        string targetAttributeValue,
        string targetAttributeName,
        string[] columnSet,
        int returnCount,
        int pageNum,
        string orderAttributeName,
        bool ascending,
        IOrganizationService service)
    {
        try
        {
            // define table query
            QueryByAttribute query = new(entityName)
            {
                ColumnSet = new ColumnSet(columnSet),
                PageInfo = new PagingInfo() { Count = returnCount, PageNumber = pageNum }
            };

            // add query parameters
            query.AddAttributeValue(targetAttributeName, targetAttributeValue);

            if (orderAttributeName != null && orderAttributeName != "")
            {
                query.AddOrder(orderAttributeName, ascending ? OrderType.Ascending : OrderType.Descending);
            }

            // send query request
            Microsoft.Xrm.Sdk.EntityCollection queryResults = service.RetrieveMultiple(query);

            return queryResults;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in GetMultipleRecords: {0}", ex);
            throw;
        }
    }

    public static void UpdateTargetAttributes(string tableName, EntityReference targetRef, Dictionary<string, object> attributeDictionary, IOrganizationService service)
    {
        try
        {
            var targetEntity = new Entity(tableName, targetRef.Id);

            foreach (var field in attributeDictionary)
            {
                targetEntity[field.Key] = field.Value;
            }

            service.Update(targetEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in UpdateTargetAttributes: {0}", ex);
            throw;
        }
    }

    /* helper to calculate new hash using a standard BKDR hashing algorithm */
    /* mirrors hashing implementation in Power Query                        */
    /* takes a concatenated string (with '|' separator), converts each      */
    /* char to its corresponding integer, adds padding and then calculates  */
    /* the hash                                                             */
    public static long ComputeBKDRHashKey(string plaintext)
    {

        long seed = 1313;
        long modulo = 2147483647;
        long state = 0;

        try
        {
            for (int i = 0; i < plaintext.Length; i++)
            {
                // convert character to 3-digit integer
                int charInt = plaintext[i];

                // convert integer into a string with padding
                string paddedStr = charInt.ToString("D3");

                // parse to long
                long current = long.Parse(paddedStr);

                // caculate state
                state = (state + (current * seed)) % modulo;
            }

            return state;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in ComputeBKDRHashKey: {0}", ex);
            throw;
        }
    }

    public static void SetDeltaAttribute<T>(Entity target, string attributeName, T newValue, T oldValue)
    {
        try
        {
            if (!EqualityComparer<T>.Default.Equals(newValue, oldValue))
            {
                target[attributeName] = newValue;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in SetDeltaAttribute: {0}", ex);
            throw;
        }
    }

    public static void SetDeltaOptionSet(Entity target, string attributeName, OptionSetValue newValue, OptionSetValue oldValue)
    {
        try
        {
            if (oldValue == null || newValue == null || oldValue.Value != newValue.Value)
            {
                target[attributeName] = newValue;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in SetDeltaOptionSet: {0}", ex);
            throw;
        }
    }

    public static void SetDeltaLookup(Entity target, string attributeName, EntityReference newValue, EntityReference oldValue)
    {
        try
        {
            if (oldValue == null || newValue == null || oldValue.Id != newValue.Id || oldValue.LogicalName != newValue.LogicalName)
            {
                target[attributeName] = newValue;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in SetDeltaLookup: {0}", ex);
            throw;
        }
    }

    public static void SetDeltaDateTime(Entity target, string attributeName, DateTime? newValue, DateTime? oldValue)
    {
        try
        {
            DateTime? newUtcDateTime = newValue?.ToUniversalTime();
            DateTime? oldUtcDateTime = oldValue?.ToUniversalTime();

            if (newUtcDateTime != oldUtcDateTime)
            {
                target[attributeName] = newValue;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in SetDeltaDateTime: {0}", ex);
            throw;
        }
    }

    public static string CreateHTTPRequest(ITracingService? tracingService, string endpointUrl, FormUrlEncodedContent? content, string? jsonPayload, int timeout, string? token, HttpMethod method)
    {
        tracingService?.Trace("Initialising new HttpClient connection");

        TimeSpan timeoutSec = TimeSpan.FromSeconds(Math.Min(10, timeout));
        string responseBody;
        HttpResponseMessage httpResponse;

        using (var httpClient = new HttpClient())
        {
            httpClient.Timeout = timeoutSec;
            httpClient.DefaultRequestHeaders.ConnectionClose = true;

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                if (method == HttpMethod.Post)
                {
                    // retrieve response
                    if (!string.IsNullOrEmpty(jsonPayload))
                    {
                        var requestBody = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                        httpResponse = httpClient.PostAsync(endpointUrl, requestBody).GetAwaiter().GetResult();
                    }
                    else if (content != null)
                    {
                        Console.WriteLine(content?.ReadAsStringAsync().Result);
                        httpResponse = httpClient.PostAsync(endpointUrl, content).GetAwaiter().GetResult();
                    }
                    else
                    {
                        httpResponse = httpClient.PostAsync(endpointUrl, null).GetAwaiter().GetResult();
                    }

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        responseBody = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        Console.WriteLine($"Response Body: {responseBody}");
                        tracingService?.Trace("Token retrieved successfully.");
                    }
                    else
                    {
                        string error = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        tracingService?.Trace("API Error status code: {0} - {1}", httpResponse.StatusCode, error);

                        Console.WriteLine("API Error status code: {0} - {1}", httpResponse.StatusCode, error);
                        throw new InvalidPluginExecutionException($"API failed: {httpResponse.ReasonPhrase}");
                    }
                }
                else if (method == HttpMethod.Get)
                {
                    httpResponse = httpClient.GetAsync(endpointUrl).GetAwaiter().GetResult();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        responseBody = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        Console.WriteLine($"Response Body: {responseBody}");
                        tracingService?.Trace("GET request successful.");
                    }
                    else
                    {
                        string error = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        tracingService?.Trace("API Error status code: {0} - {1}", httpResponse.StatusCode, error);
                        Console.WriteLine("API Error status code: {0} - {1}", httpResponse.StatusCode, error);
                        throw new InvalidPluginExecutionException($"API failed: {httpResponse.ReasonPhrase}");
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException($"HTTP method {method} not supported.");
                }
            }
            catch (AggregateException e)
            {
                foreach (var exception in e.Flatten().InnerExceptions)
                {
                    tracingService?.Trace("Network exception: {0}", exception.Message);
                }
                throw new InvalidPluginExecutionException($"Network error: {e.Message}");
            }
            catch (Exception e)
            {
                tracingService?.Trace("An exception was thrown: {0}", e.Message);
                throw new InvalidPluginExecutionException($"An exception was thrown: {e.Message}");
            }
        }

        return responseBody;
    }

    public static string RetrieveBearerToken(ITracingService? tracingService, int timeout, List<KeyValuePair<string, string>> parameterList, string tenantId)
    {
        string tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

        var content = new FormUrlEncodedContent(parameterList);

        string response = CreateHTTPRequest(tracingService, tokenEndpoint, content, null, timeout, null, HttpMethod.Post);

        var jsonResponse = JObject.Parse(response);

        if (jsonResponse != null && jsonResponse["access_token"] != null)
        {
            return jsonResponse["access_token"]?.ToString() ?? throw new InvalidPluginExecutionException("Empty token property.");
        }
        else
        {
            tracingService?.Trace($"Token payload evaluation failed. Raw response: {response}");
            throw new InvalidPluginExecutionException("Failed to retrieve bearer token.");
        }
    }

    public static async Task<string> RetrieveBearerToken(ITracingService? tracingService, IPublicClientApplication? app)
    {

        var _tracingService = tracingService;
        IPublicClientApplication _app = app ?? throw new ArgumentNullException(nameof(app));

        string[] scopes = new[] { "User.Read" };

        var result = await _app.AcquireTokenWithDeviceCode(scopes, callback =>
        {
            Console.WriteLine(callback.Message);
            _tracingService?.Trace(callback.Message);

            return Task.CompletedTask;
        }).ExecuteAsync();

        _tracingService?.Trace($"Token acquired: {result.AccessToken}");

        var _result = result.AccessToken ?? throw new InvalidPluginExecutionException(nameof(result));

        return _result;
    }

    public static string GetUserAzureId(ITracingService? tracingService, string upn, string groupId, string token, bool adminConsentGranted)
    {
        var _upn = upn ?? throw new ArgumentNullException(nameof(upn));
        var _groupId = groupId ?? throw new ArgumentNullException(nameof(groupId));
        var _token = token ?? throw new ArgumentNullException(nameof(token));

        string endpointUrl;

        if (adminConsentGranted)
        {
            endpointUrl = $"https://graph.microsoft.com/v1.0/users/{_upn}";
        }
        else
        {
            endpointUrl = $"https://graph.microsoft.com/v1.0/me";
        }
        ;

        new KeyValuePair<string, string>("scope", $"https://graph.microsoft.com");

        Console.WriteLine($"Constructing request to graph api using endpoint: {endpointUrl}");

        var responseJson = CreateHTTPRequest(tracingService, endpointUrl, null, null, 5, _token, HttpMethod.Get);

        Console.WriteLine($"Response Json: {responseJson.ToString()}");

        var jsonObject = JsonDocument.Parse(responseJson);
        string objectId = jsonObject.RootElement.GetProperty("id").GetString() ?? throw new InvalidPluginExecutionException("User ID not found in response.");

        return objectId;
    }

    /// <summary>
    /// 
    /// </summary>
    public class ServiceInitialiser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static ITracingService InitTracingService(System.IServiceProvider provider, bool debug)
        {
            var tracingService = (ITracingService)provider.GetService(typeof(ITracingService));

            if (debug) tracingService.Trace("Tracing service initialised");

            return tracingService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="tracer"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static IOrganizationServiceFactory InitServiceFactory(System.IServiceProvider provider, ITracingService tracer, bool debug)
        {
            var orgServiceFactory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));

            if (debug) tracer.Trace("Organisation service factory initialised");

            return orgServiceFactory;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="factory"></param>
        /// <param name="tracer"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static IOrganizationService InitOrganisationService(IPluginExecutionContext context, IOrganizationServiceFactory factory, ITracingService tracer, bool debug)
        {
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            if (debug) tracer.Trace("Organisation service factory initialised");

            return service;
        }
    }

    /*public static string CheckGroupMembership(string azureId, string groupId, string bearerToken)
    {
        
    }*/
}
