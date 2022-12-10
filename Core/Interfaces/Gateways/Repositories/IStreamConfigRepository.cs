using Core.DataTransferObjects;

namespace Core.Interfaces.Gateways.Repositories
{
    public interface IStreamConfigRepository
    {
        IEnumerable<StreamConfigDto> GetStreamConfigs();
    }
}