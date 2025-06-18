
using ConstrainService.Application.DTOs.Constrain;

namespace ConstrainService.Application.Interfaces.Service
{
    public interface IConstrainServiceProvider
    {
        Task AddConstrain(AddConstrainDto addConstrainDto);
        Task<ReadConstrainDto> GetConstrainById(string id);
        Task<List<ReadConstrainDto>> GetAllConstrain();
    }
}