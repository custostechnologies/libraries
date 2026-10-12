public interface IDataverseLogger
{
    void WriteLog(string message);
    void WriteLog(string message, params object?[] args);
    void WriteLog(Exception ex, string message);
    void WriteLog(Exception ex, string message, params object?[] args);
}