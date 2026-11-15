using System.Text;
using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Infrastructure.Dataverse;

namespace Custos.XrmHelpers.Infrastructure.Telemetry
{
    public class TelemetryRepository : ITelemetryRepository, IDisposable
    {
        private readonly IPluginExecutionContext _executionContext;
        private readonly IOrganizationService _userService;
        private readonly StringBuilder _logBuilder = new();
        public TelemetryRepository(
            IPluginExecutionContext executionContext,
            IOrganizationService userService
        )
        {
            _executionContext = executionContext;
            _userService = userService;
        }
        public void SaveLog(string message)
        {
            _logBuilder.AppendLine(message);
        }
        public Guid CreateLog()
        {
            // prevent accidental recursion if registered on same tbale
            if (_executionContext.PrimaryEntityName == "mthomas_pluginlog") return Guid.Empty;
            return PrepareLog();
        }
        private Guid PrepareLog()
        {

            var log = new Entity("mthomas_pluginlog");
            log["mthomas_correlationid"] = _executionContext.CorrelationId.ToString();
            log["mthomas_depth"] = _executionContext.Depth;
            log["mthomas_message"] = _executionContext.MessageName;
            log["mthomas_mode"] = _executionContext.Mode.ToString();
            log["mthomas_pluginstepid"] = _executionContext.OwningExtension?.Id.ToString();
            log["mthomas_requestid"] = _executionContext.RequestId.ToString();
            log["mthomas_starttime"] = _executionContext.OperationCreatedOn;
            log["mthomas_pluginname"] = _executionContext.OwningExtension?.Name;
            log["mthomas_log"] = _logBuilder.ToString();

            return _userService.Create(log);
        }
        public void Dispose()
        {
            if (_logBuilder.Length > 0) CreateLog();
        }
    }
}