using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Infrastructure.Graph
{
    public class EmailService
    {
        private readonly IDataverseContext _context;
        private IDataverseLogger _logger;
        public EmailService(
            IDataverseLoggerFactory loggerFactory,
            IDataverseContext context
        )
        {
            _context = context;
            _logger = loggerFactory.Create<EmailService>();
        }
        private string GetGraphToken(
            string tid,
            string cid,
            string secret
        )
        {
            _logger.WriteLog(
                "Starting GetGraphToken. TenantId={0}, ClientId={1}",
                tid,
                cid
            );

            string tenantId = tid;
            string clientId = cid;
            string clientSecret = secret;

            string tokenUrl =
                $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    _logger.WriteLog(
                        "Configured HttpClient. Timeout={0}s",
                        client.Timeout.TotalSeconds
                    );

                    var keyPairs = new[]
                    {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

                    _logger.WriteLog("Preparing token request to {0}", tokenUrl);

                    var req = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
                    {
                        Content = new FormUrlEncodedContent(keyPairs)
                    };

                    req.Headers.ConnectionClose = true;

                    _logger.WriteLog("Sending token request.");

                    HttpResponseMessage resp = client.SendAsync(req).GetAwaiter().GetResult();
                    string response = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    _logger.WriteLog(
                        "Token endpoint responded. StatusCode={0}",
                        (int)resp.StatusCode
                    );

                    if (!resp.IsSuccessStatusCode)
                    {
                        _logger.WriteLog("Token request failed. Response={0}", response);
                        throw new InvalidPluginExecutionException($"Token generation failed: {resp.StatusCode}");
                    }

                    using (JsonDocument doc = JsonDocument.Parse(response))
                    {
                        _logger.WriteLog("Successfully parsed token response.");

                        var token = doc.RootElement
                            .GetProperty("access_token")
                            .GetString();

                        if (string.IsNullOrWhiteSpace(token))
                        {
                            _logger.WriteLog("Access token missing from response.");
                            throw new InvalidPluginExecutionException();
                        }

                        _logger.WriteLog("Access token retrieved successfully.");

                        // token assured to be not null due to nullOrWhitespace check above
                        return token!;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(
                    ex,
                    "Exception encountered while retrieving Graph access token."
                );
                throw;
            }
        }
        public bool SendEmailViaApi(
            string tid,
            string cid,
            string secret,
            string content,
            string to,
            string cc,
            string from,
            string subject
        )
        {
            _logger.WriteLog("Starting SendEmailViaApi.");

            string token = GetGraphToken(tid, cid, secret);

            _logger.WriteLog("Graph access token acquired.");

            string endpoint = $"https://graph.microsoft.com/v1.0/users/{from}/sendMail";

            var toRecipients = to.Split(';')
                .Select(
                    email => new
                    {
                        emailAddress = new
                        {
                            address = email.Trim()
                        }
                    }
                )
                .ToArray();

            if (string.IsNullOrWhiteSpace(toRecipients[0].emailAddress.address) || toRecipients == null)
            {
                throw new InvalidPluginExecutionException("Unable to parse primary recipients.");
            }

            var ccRecipients = cc.Split(';')
                .Select(
                    email => new
                    {
                        emailAddress = new
                        {
                            address = email.Trim()
                        }
                    }
                )
                .ToArray();

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    _logger.WriteLog("HttpClient configured. Endpoint={0}", endpoint);

                    var req = new HttpRequestMessage(HttpMethod.Post, endpoint);

                    req.Headers.Authorization =
                        new AuthenticationHeaderValue(
                            "Bearer",
                            token
                        );

                    req.Headers.ConnectionClose = true;

                    var payload = new
                    {
                        message = new
                        {
                            subject,
                            body = new
                            {
                                contentType = "HTML",
                                content
                            },
                            toRecipients
                        },
                        saveToSentItems = "true"
                    };

                    _logger.WriteLog("Created email payload. ContentLength={0}", content?.Length ?? 0);

                    string jsonPayload = JsonSerializer.Serialize(payload);

                    _logger.WriteLog("Serialized payload. JsonLength={0}", jsonPayload.Length);

                    req.Content = new StringContent(
                        jsonPayload,
                        Encoding.UTF8,
                        "application/json"
                    );

                    _logger.WriteLog("Sending Graph sendMail request.");

                    HttpResponseMessage response = client.SendAsync(req).GetAwaiter().GetResult();

                    string responseString = response.Content
                        .ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult();

                    _logger.WriteLog("Graph response received. StatusCode={0}", (int)response.StatusCode);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.WriteLog("Graph sendMail failed. Response={0}", responseString);

                        throw new Exception(
                            $"Failed to send email via Graph: " +
                            $"{response.StatusCode} - {responseString}"
                        );
                    }

                    _logger.WriteLog("Email sent successfully via Microsoft Graph API.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex, "Exception encountered while sending email via Microsoft Graph.");
                throw;
            }
        }
    }
}