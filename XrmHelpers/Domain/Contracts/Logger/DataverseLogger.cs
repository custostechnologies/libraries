using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

public sealed class DataverseLogger : IDataverseLogger
{
    private readonly ITelemetryRepository _telemetryRepository;
    private readonly ITracingService _tracer;
    private readonly ILogger _telemetry;
    private readonly string _className;

    public DataverseLogger(
        ITracingService tracer,
        ILogger telemetry,
        ITelemetryRepository telemetryRepository,
        string className)
    {
        _tracer = tracer;
        _telemetryRepository = telemetryRepository;
        _telemetry = telemetry;
        _className = className;
    }

    public void WriteLog(string message)
    {
        var formatted = $"[{_className}] {message}";

        _tracer.Trace(formatted);
        _telemetry.LogInformation(formatted);
        _telemetryRepository.SaveLog(formatted);
    }

    public void WriteLog(Exception ex, string message)
    {
        var formatted = $"[{_className}] {message}";

        _tracer.Trace($"{formatted}\n{ex}");
        _telemetryRepository.SaveLog(formatted);

        _telemetry.LogError(
            new EventId(5000, "UnhandledException"),
            ex,
            formatted);
    }
    public void WriteLog(string message, params object?[] args)
    {
        try
        {
            message ??= string.Empty;
            args ??= Array.Empty<object?>();

            string formattedMessage;

            try
            {
                formattedMessage = args.Length > 0
                    ? string.Format(message, args)
                    : message;
            }
            catch
            {
                formattedMessage = message;
            }

            formattedMessage = $"[{_className}] {formattedMessage}";

            // Always write to plugin trace log
            _tracer.Trace(formattedMessage);
            _telemetryRepository.SaveLog(formattedMessage);

            // Best-effort telemetry
            try
            {
                _telemetry.LogInformation(
                    "[{ClassName}] " + message,
                    [_className, .. args]
                );
            }
            catch
            {
                // Never fail because telemetry failed
            }
        }
        catch
        {
            // Logging must never break plugin execution
        }
    }
}