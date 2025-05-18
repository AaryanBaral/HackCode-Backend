using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.Application.DTOs.ResponseDto
{
    public class APIResponse<T>
    {
        public required T Data {get; set;}
        public bool Success {get; set;} = true;
    }
}