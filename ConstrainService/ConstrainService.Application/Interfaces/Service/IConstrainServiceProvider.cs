
using ConstrainService.Application.DTOs.Constrain;

namespace ConstrainService.Application.Interfaces.Service
{
    public interface IConstrainServiceProvider
    {
        Task AddConstrain(AddConstrainDto addConstrainDto);
    }
}