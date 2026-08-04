using spgsWebTools.Infrastructure.Http;

namespace spgsWebTools.Infrastructure.Graph
{
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
}