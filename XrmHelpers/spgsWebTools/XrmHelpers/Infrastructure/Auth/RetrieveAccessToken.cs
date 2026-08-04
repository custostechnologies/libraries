using System;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Http;

namespace spgsWebTools.Infrastructure.Auth
{
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
}