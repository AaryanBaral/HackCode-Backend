using ConstrainService.Application.DTOs.Constrain;
using ConstrainService.Application.Interfaces.Kafka;
using ConstrainService.Application.Interfaces.Repository;
using ConstrainService.Application.Interfaces.Service;
using ConstrainService.Application.Mapper;

namespace ConstrainService.Application.Services
{
    public class ConstrainServiceProvider(IConstrainRepository constrainRepository, IQuestionValidator questionValidator) : IConstrainServiceProvider
    {
        private readonly IConstrainRepository _constrainRepository = constrainRepository;
        private readonly IQuestionValidator _questionValidator = questionValidator;

        public async Task AddConstrain(AddConstrainDto addConstrainDto)
        {

            var isQuestionValid = await _questionValidator.ValidateQuestionId(addConstrainDto.QuestionId);
            if (!isQuestionValid) throw new KeyNotFoundException("Question id not valid");

            var constrain = addConstrainDto.ToConstrain();
            await _constrainRepository.AddConstrain(constrain);

        }

        public async Task<ReadConstrainDto> GetConstrainById(string id)
        {
            var constrain = await _constrainRepository.GetConstrainById(id);
            return constrain.ToReadConstrain();
        }
        public async Task<List<ReadConstrainDto>> GetAllConstrain()
        {
            var constrain = await _constrainRepository.GetAllConstrain();
            return [.. constrain.Select(c => c.ToReadConstrain())];
        }
    }
}