using Core.DataTransferObjects;
using Core.Interfaces.Gateways.Repositories;

namespace Infastructure.Data.Mock
{
    public class StreamConfigRepositoryMock : IStreamConfigRepository
    {
        public IEnumerable<StreamConfigDto> GetStreamConfigs()
        {
            return new[]
            {
                new StreamConfigDto(1, "Test 1"),
                new StreamConfigDto(2, "Test 2") 
            };
        }
    }
}