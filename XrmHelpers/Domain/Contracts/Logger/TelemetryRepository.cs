using System.Text;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
public class TelemetryRepository : ITelemetryRepository, IDisposable
{
    private readonly IDataverseContext _context;
    private readonly StringBuilder _logBuilder = new();
    public TelemetryRepository(IDataverseContext dataverseContext)
    {
        _context = dataverseContext;
    }
    public void SaveLog(string message)
    {
        _logBuilder.AppendLine(message);
    }
    public Guid CreateLog()
    {
        // prevent accidental recursion if registered on same tbale
        if (_context.Runtime.ExecutionContext.PrimaryEntityName == "mthomas_pluginlog") return Guid.Empty;
        return PrepareLog();
    }
    private Guid PrepareLog()
    {
        var executionContext = _context.Runtime.ExecutionContext;
        var userService = _context.Runtime.UserService;
        
        var log = new Entity("mthomas_pluginlog");
        log["mthomas_correlationid"] = executionContext.CorrelationId;
        log["mthomas_depth"] = executionContext.Depth;
        log["mthomas_message"] = executionContext.MessageName;
        log["mthomas_mode"] = executionContext.Mode;
        log["mthomas_pluginstepid"] = executionContext.OwningExtension?.Id;
        log["mthomas_requestid"] = executionContext.RequestId;
        log["mthomas_starttime"] = executionContext.OperationCreatedOn;
        log["mthomas_pluginname"] = executionContext.OwningExtension?.Name;
        log["mthomas_log"] = _logBuilder.ToString();

        return userService.Create(log);
    }
    public void Dispose()
    {
        if (_logBuilder.Length > 0) CreateLog();
    }
}