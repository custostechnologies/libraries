using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Xrm.Sdk;
using System.Text.Json;

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
        public HttpContent TriggerAnonFlow(
            string endpoint,
            string upn
        )
        {
            using (var client = new HttpClient())
            {

                var payload = $@"{{
                    ""upn"": ""{upn}""
                }}";

                var content = new StringContent(
                    payload,
                    Encoding.UTF8,
                    "application/json");

                var response = client.PostAsync(endpoint, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        response.Content.ReadAsStringAsync().Result;

                    throw new Exception(
                        $"Flow call failed. Status: {response.StatusCode}. Response: {error}");
                }

                return response.Content;
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