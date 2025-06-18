using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCaseService.Application.Interfaces.Kafka
{
    public interface IQuestionValidator
    {
            Task<bool> ValidateQuestionId(string id);
    }
}