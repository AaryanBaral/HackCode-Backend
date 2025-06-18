
using ConstrainService.Domain.Entities;

namespace ConstrainService.Application.Interfaces.Repository
{
    public interface IConstrainRepository
    {

        Task AddConstrain(Constrain constrain);
        Task<List<Constrain>> GetAllConstrain();
        Task<Constrain> GetConstrainById(string id);
    }
}