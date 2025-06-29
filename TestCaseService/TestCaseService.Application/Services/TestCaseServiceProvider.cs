using TestCaseService.Application.DTOs.TetstCase;
using TestCaseService.Application.Interfaces.Kafka;
using TestCaseService.Application.Interfaces.Service;

namespace TestCaseService.Application.Services
{
    public class TestCaseServiceProvider(IQuestionValidator questionValidator):ITestCaseService
    {
        private readonly IQuestionValidator _questionValidator = questionValidator;
        public async Task AddConstrain(CreateTestCaseDto addTestCaseDto)
        {
            var isValid = await _questionValidator.ValidateQuestionId(addTestCaseDto.QuestionId);
            if (!isValid) throw new KeyNotFoundException("Given Question Doesnot exists");
            
        }
    }
}