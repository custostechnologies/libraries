using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Azure.Functions.Worker.Http;

namespace spgsWebTools.FunctionHelpers.Utilities
{
    public static class AuthorisationHeader
    {
        public static string Parse(HttpRequestData req)
        {
            if (!req.Headers.TryGetValues("Authorization", out var values))
            {
                return "";
            }
            else
            {
                var authHeader = values.FirstOrDefault();
                if (authHeader == null || string.IsNullOrWhiteSpace(authHeader["Bearer ".Length..]))
                {
                    return "";
                }
                return authHeader["Bearer ".Length..];
            }
        }
    }
}