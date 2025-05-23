using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageService.Application.DTOs.APIResponse
{
    public class APIResponse<T>
    {
        public  T? Data { get; set; }
        public bool Success { get; set; } = true;
    }
}