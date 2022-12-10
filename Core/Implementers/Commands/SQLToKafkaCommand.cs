using Core.Interfaces;
using Core.DataTransferObjects;

namespace Core.Implementers.Commands
{
    public class SQLToKafkaCommand : ICommand
    {
        private readonly ILogger _logger;

        public SQLToKafkaCommand(ILogger logger)
        {
            _logger = logger;
        }
        public void Execute(object item)
        {
            StreamConfigDto streamConfigDto = (StreamConfigDto)item;
            _logger.Log("SQL To Kafka command has started");
            _logger.Log($"Processing stream {System.Text.Json.JsonSerializer.Serialize(streamConfigDto)}");
            _logger.Log("SQL To Kafka command has completed");
        }
    }
}