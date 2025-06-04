using ConstrainService.Application.DTOs.Constrain;
using ConstrainService.Application.Interfaces.Repository;
using ConstrainService.Application.Mapper;

namespace ConstrainService.Application.Services
{
    public class ConstrainServiceProvider(IConstrainRepository constrainRepository)
    {
        private readonly IConstrainRepository _constrainRepository = constrainRepository;

        public async Task AddConstrain(AddConstrainDto addConstrainDto)
        {
            // code to validate the questio id using kafka call

            var constrain = addConstrainDto.ToConstrain();
            // call for reposptory for adding it to the database after all validations
            await _constrainRepository.AddConstrain(constrain);
            
        }
    }
}