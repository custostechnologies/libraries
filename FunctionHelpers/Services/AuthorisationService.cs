using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using custos.FunctionHelpers.Types;
using custos.FunctionHelpers.Services;
using custos.XrmHelpers.Domain.Contracts;
using System.Security.Claims;

namespace custos.FunctionHelpers.Services
{
    public class AuthorisationService : IAuthorisationService
    {
        private readonly ILogger<AuthorisationService> _logger;
        private readonly IConfiguration _config;
        private readonly string _expectedTenantId;
        private readonly string _expectedScope;
        private readonly string _requiredAppRole;
        private readonly string _clientId;
        private readonly HashSet<string> _allowedClients;
        private readonly string[] _validIssuers;
        private readonly string[] _validAudiences;
        private readonly ConfigurationManager<OpenIdConnectConfiguration> _configManager;
        private readonly HashSet<string> _printClaims = new HashSet<string>()
        {
            "tid",
            "aud",
            "azp",
            "appid",
            "oid",
            "scp",
            "roles"
        };
        public AuthorisationService(ILogger<AuthorisationService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            _logger.LogInformation("Entered AuthorisationSerice");

            _expectedTenantId = _config["TENANT_ID"] ?? "";
            _logger.LogInformation($"_expectedTenantId: {_expectedTenantId}");

            _clientId = _config["CLIENT_ID"] ?? "";
            _logger.LogInformation($"_clientId: {_clientId}");

            _validAudiences = [
                $"api://{_clientId}",
                _clientId
            ];
            _logger.LogInformation($"_validAudiences: {_validAudiences}");

            _expectedScope = _config["EXPECTED_SCOPE"] ?? "user_impersonation";
            _logger.LogInformation($"_validAudiences: {_validAudiences}");

            _requiredAppRole = _config["REQUIRED_APP_ROLE"] ?? "Api.Access";
            _logger.LogInformation($"_requiredAppRole: {_requiredAppRole}");

            var allowedClientsString = _config["ALLOWED_CLIENTS"] ?? "";
            _logger.LogInformation($"allowedClientsString: {allowedClientsString}");

            _allowedClients = allowedClientsString
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => id.Trim())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            _logger.LogInformation($"allowedClientsString: {allowedClientsString}");

            _validIssuers = [
                $"https://login.microsoftonline.com/{_expectedTenantId}/v2.0",
                $"https://sts.windows.net/{_expectedTenantId}/"
            ];
            _logger.LogInformation($"_validIssuers: {_validIssuers}");
            _logger.LogInformation("Instantiating _configManager");
            _configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                $"https://login.microsoftonline.com/{_expectedTenantId}/v2.0/.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever()
            );
        }
        public async Task<IAuthValidationResult> ValidateIncomingRequestAsync(string token)
        {
            _logger.LogInformation("Entered ValidateIncomingRequestAsync");

            bool appToken = false;
            var configuration = await _configManager.GetConfigurationAsync();

            // log configuration information
            _logger.LogInformation("Loaded {Count} signing keys", configuration.SigningKeys.Count);
            foreach (var key in configuration.SigningKeys)
            {
                _logger.LogInformation("Key ID: {Kid}", key.KeyId);
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = _validIssuers,
                ValidateAudience = true,
                ValidAudiences = _validAudiences,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = configuration.SigningKeys,
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            var jwtToken = new JwtSecurityToken(token);

            // log token metadata
            _logger.LogInformation("iss: {Iss}", jwtToken.Issuer);
            _logger.LogInformation("kid: {Kid}", jwtToken.Header.Kid);
            _logger.LogInformation("aud: {Aud}", string.Join(",", jwtToken.Audiences));
            _logger.LogInformation("ver: {Ver}", jwtToken.Claims.FirstOrDefault(claim => claim.Type == "ver")?.Value);

            var tokenHandler = new JwtSecurityTokenHandler()
            {
                MapInboundClaims = false
            };

            _logger.LogInformation("Calling tokenHandler.ValidateToken");

            ClaimsPrincipal claimsPrincipal;

            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Initial JWT token validation failed.");
                if (ex.InnerException != null)
                {
                    _logger.LogInformation($"Inner Exception: {ex.InnerException}");
                }
                throw;
            }

            var scp = claimsPrincipal.FindFirst("scp")?.Value;
            var roles = claimsPrincipal.FindAll("roles")
                .Select(role => role.Value)
                .ToList();
            var clientId = claimsPrincipal.FindFirst("azp")?.Value ?? claimsPrincipal.FindFirst("appid")?.Value;
            var tenantId = claimsPrincipal.FindFirst("tid")?.Value;

            foreach (var claim in claimsPrincipal.Claims)
            {
                if (_printClaims.Contains(claim.Type)) _logger.LogInformation($"foreach -- Claim {claim.Type}: {claim.Value}");
            }

            // PERFORM CUSTOM CLAIM VALIDATIONS
            // validate azp (ALLOWED CLIENTS)
            if (string.IsNullOrWhiteSpace(clientId) || !_allowedClients.Contains(clientId)) throw new UnauthorizedAccessException("Client/App ID is not included in the allowed clients list.");

            // validate scp
            if (!string.IsNullOrWhiteSpace(scp))
            {
                if (!scp.Split(' ').Contains(_expectedScope))
                {
                    throw new UnauthorizedAccessException($"Required scope: {_expectedScope} was not found.");
                }
            }
            else if (roles.Any())
            {
                appToken = true;
                if (!roles.Contains(_requiredAppRole))
                {
                    throw new UnauthorizedAccessException($"Required app role: {_requiredAppRole} was not found in claims.");
                }
            }
            else
            {
                _logger.LogInformation($"scp in else branch: {scp}");
                throw new UnauthorizedAccessException("Token type is not supported.");
            }

            if (tenantId != _expectedTenantId) throw new UnauthorizedAccessException("Tenant id did not match expected tenant id.");

            IAuthValidationResult result = new AuthValidationResult
            {
                ClaimsPrincipal = claimsPrincipal,
                ClientId = clientId,
                IsAppToken = appToken,
                IsDelegatedToken = !appToken,
                AccessToken = token
            };

            return result;
        }
    }
}