using Core.Helpers.EnumHelpers;

namespace Core.Interfaces
{
    public interface ILogger
    {
        void Log(string message, LogSeverity logSeverity = LogSeverity.INFO);  
        void LogException(Exception exception, LogSeverity logSeverity = LogSeverity.ERROR);       
    }
}