using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionService.Application.DTOs.QuestionDto;

namespace QuestionService.Application.Interfaces
{
    public interface IQuestionService
    {
        Task<bool> AddQuestionAsync(AddQuestionDto addQuestionDto, string userID);
        Task<bool> TestKafka();
        Task<bool> UpdateQuestion(UpdateQuestionDto updateQuestionDto, string id);
    }
}