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
        private DataverseLogger _logger;
        public EmailService(
            IDataverseContext context
        )
        {
            _context = context;
            _logger = _context.GetLogger<EmailService>();
        }
        private string GetGraphToken(
            string tid,
            string cid,
            string secret
        )
        {
            string tenantId = tid;
            string clientId = cid;
            string clientSecret = secret;
            string tokenUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5);

                var keyPairs = new[]
                {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("scope", "Mail.Send"),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                var req = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
                {
                    Content = new FormUrlEncodedContent(keyPairs)
                };
                req.Headers.ConnectionClose = true;

                HttpResponseMessage resp = client.SendAsync(req).GetAwaiter().GetResult();
                string response = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (!resp.IsSuccessStatusCode) throw new InvalidPluginExecutionException($"Token generation failed: {resp.StatusCode}");

                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    var token = doc.RootElement.GetProperty("access_token").GetString();
                    if (string.IsNullOrWhiteSpace(token)) throw new InvalidPluginExecutionException();
                    return doc.RootElement.GetProperty("access_token").GetString()!;
                }
            }
        }
        public bool SendEmailViaApi(
            string tid,
            string cid,
            string secret,
            string content
        )
        {
            string token = GetGraphToken(tid, cid, secret);

            string endpoint = "https://graph.microsoft.com/v1.0/me/sendMail";

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5);

                var req = new HttpRequestMessage(HttpMethod.Post, endpoint);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                req.Headers.ConnectionClose = true;

                var payload = new
                {
                    message = new
                    {
                        subject = "Automated Alert from Dataverse Plug-in",
                        body = new
                        {
                            contentType = "HTML",
                            content
                        },
                        toRecipients = new[]
                        {
                            new { emailAddress = new { address = "matthew.thomas@stpauls.nsw.edu.au" } }
                        }
                    },
                    saveToSentItems = "true"
                };

                string jsonPayload = JsonSerializer.Serialize(payload);
                req.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(req).GetAwaiter().GetResult();
                string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to send email via Graph: {response.StatusCode} - {responseString}");
                }

                _logger.WriteLog("Email sent successfully via Microsoft Graph API.");
                return true;
            }
        }
    }
}