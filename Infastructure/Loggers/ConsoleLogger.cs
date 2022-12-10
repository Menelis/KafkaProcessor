using Core.Helpers.EnumHelpers;
using Core.Interfaces;
using Core.Extensions;
using Newtonsoft.Json;

namespace Infastructure.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, LogSeverity logSeverity = LogSeverity.INFO)
        {
           Console.WriteLine($"{DateTime.Now} - {logSeverity.GetEnumStringValue()} - {message}");
        }

        public void LogException(Exception exception, LogSeverity logSeverity = LogSeverity.ERROR)
        {
            Console.WriteLine($"{DateTime.Now} - {logSeverity.GetEnumStringValue()} - {JsonConvert.SerializeObject(exception)}");
        }
    }
}