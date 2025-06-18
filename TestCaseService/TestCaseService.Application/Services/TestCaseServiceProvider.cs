using TestCaseService.Application.DTOs.TetstCase;
using TestCaseService.Application.Interfaces.Kafka;

namespace TestCaseService.Application.Services
{
    public class TestCaseServiceProvider(IQuestionValidator questionValidator)
    {
        private readonly IQuestionValidator _questionValidator = questionValidator;
        public async Task AddConstrain(AddTestCaseDto addTestCaseDto)
        {
            var isValid = await _questionValidator.ValidateQuestionId(addTestCaseDto.QuestionId);
            if (!isValid) throw new KeyNotFoundException("Given Question Doesnot exists");
            
        }
    }
}