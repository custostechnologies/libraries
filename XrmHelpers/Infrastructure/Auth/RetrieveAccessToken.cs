using System;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Identity.Client;
using spgsWebTools.XrmHelpers.Infrastructure.Http;

namespace spgsWebTools.Infrastructure.Auth
{
    public class RetrieveAccessToken
    {
        /*public static string Execute(ITracingService? tracingService, string tenantId, string clientId, string clientSecret, string scope)
        {
            string tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

            var content = new FormUrlEncodedContent(parameterList);

            string token = CreateHttpRequest.Execute(tracingService, tokenEndpoint, content, null, timeout, null, HttpMethod.Post);

            if (token != null)
            {
                return token;
            }
            else
            {
                tracingService?.Trace($"RetrieveAccessToken.Execute() did not return a token.");
                throw new InvalidPluginExecutionException("Failed to retrieve access token.");
            }
        }*/
        public static string Execute(ITracingService? tracingService, int timeout, List<KeyValuePair<string, string>> parameterList, string tenantId)
        {
            string tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

            var content = new FormUrlEncodedContent(parameterList);

            string token = CreateHttpRequest.Execute(tracingService, tokenEndpoint, content, null, timeout, null, HttpMethod.Post);

            if (token != null)
            {
                return token;
            }
            else
            {
                tracingService?.Trace($"RetrieveAccessToken.Execute() did not return a token.");
                throw new InvalidPluginExecutionException("Failed to retrieve access token.");
            }
        }

        public static async Task<string> Execute(ITracingService? tracingService, IPublicClientApplication? app)
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
    }
}