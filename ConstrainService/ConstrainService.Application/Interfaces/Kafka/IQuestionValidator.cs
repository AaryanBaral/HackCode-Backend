

namespace ConstrainService.Application.Interfaces.Kafka
{
    public interface IQuestionValidator
    {

    Task<bool> ValidateQuestionId(string id);
    }
}