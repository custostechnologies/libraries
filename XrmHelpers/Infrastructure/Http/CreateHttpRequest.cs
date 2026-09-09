using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Xrm.Sdk;
using System.Text.Json;
using System.Net;

namespace spgsWebTools.XrmHelpers.Infrastructure.Http
{
    public class AccessToken
    {
        public string? Token { get; set; }
    }
    public class CreateHttpRequest
    {
        private readonly ITracingService _tracer;
        public CreateHttpRequest(
            ITracingService tracer
        )
        {
            _tracer = tracer;
        }
        public (HttpStatusCode status, string body) TriggerAnonFlow(
            string endpoint,
            string upn,
            string correlationId
        )
        {
            using (var client = new HttpClient())
            {

                var payload = $@"{{
                    ""upn"": ""{upn}"",
                    ""correlationId"": ""{correlationId}""
                }}";

                var content = new StringContent(
                    payload,
                    Encoding.UTF8,
                    "application/json");

                var response = client.PostAsync(endpoint, content).Result;

                if (response != null)
                {
                    _tracer.Trace("Flow response (client.PostAsync().Result): {0}", JsonSerializer.Serialize(response));

                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    _tracer.Trace("Flow Response Body (response.Content.ReadAsStringAsync().Result): {0}", JsonSerializer.Serialize(responseBody));

                    if (!response.IsSuccessStatusCode)
                    {
                        var error =
                            response.Content.ReadAsStringAsync().Result;

                        throw new Exception(
                            $"Flow call failed. Status: {response.StatusCode}. Response: {error}");
                    }

                    _tracer.Trace("Flow response received successfully. Returning (response.StatusCode, responseBody) to calling method.");

                    return (response.StatusCode, responseBody);
                }
                return (HttpStatusCode.SeeOther, "");
            }
        }
        /*
        public string PostAnonymous(string endpointUrl, string jsonPayload, int timeout)
        {
            _tracer.Trace("Initialising new HttpClient connection");

            TimeSpan timeoutSec = TimeSpan.FromSeconds(Math.Min(10, timeout));
            string responseBody;
            HttpResponseMessage httpResponse;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = timeoutSec;
                httpClient.DefaultRequestHeaders.ConnectionClose = true;

                try
                {
                    // retrieve response
                    var requestBody = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    httpResponse = httpClient.PostAsync(endpointUrl, requestBody).GetAwaiter().GetResult();

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        responseBody = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        Console.WriteLine($"Response Body: {responseBody}");
                        _tracer.Trace("Result retrieved successfully.");
                    }
                    else
                    {
                        string error = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        _tracer.Trace("API Error status code: {0} - {1}", httpResponse.StatusCode, error);

                        Console.WriteLine("API Error status code: {0} - {1}", httpResponse.StatusCode, error);
                        throw new InvalidPluginExecutionException($"API failed: {httpResponse.ReasonPhrase}");
                    }
                }
                catch (AggregateException e)
                {
                    foreach (var exception in e.Flatten().InnerExceptions)
                    {
                        _tracer.Trace("Network exception: {0}", exception.Message);
                    }
                    throw new InvalidPluginExecutionException($"Network error: {e.Message}");
                }
                catch (Exception e)
                {
                    _tracer.Trace("An exception was thrown: {0}", e.Message);
                    throw new InvalidPluginExecutionException($"An exception was thrown: {e.Message}");
                }
            }

            return responseBody;
        }*/
        public static string Execute(ITracingService? tracingService, string endpointUrl, FormUrlEncodedContent? content, string? jsonPayload, int timeout, string? token, HttpMethod method)
        {
            tracingService?.Trace("Initialising new HttpClient connection");

            TimeSpan timeoutSec = TimeSpan.FromSeconds(Math.Min(10, timeout));
            string responseBody;
            AccessToken? accessToken = new AccessToken();
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
                            accessToken = JsonSerializer.Deserialize<AccessToken>(responseBody);
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

            return accessToken?.Token ?? throw new InvalidPluginExecutionException("No token found.");
        }
    }
}