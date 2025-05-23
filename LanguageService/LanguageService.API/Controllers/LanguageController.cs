using System;
using LanguageService.Application.DTOs.APIResponse;
using LanguageService.Application.DTOs.LanguageDto;
using LanguageService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanguageService.API.Controllers
{
    [Route("api/[controller]")]

    public class LanguageController(ILogger<LanguageController> logger, ILanguageService service) : Controller
    {
        private readonly ILogger<LanguageController> _logger = logger;
        private readonly ILanguageService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllLanguages([FromQuery] string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var allLanguage = await _service.GetAllLanguage();
                return Ok(new APIResponse<List<ReadLanguageDto>>()
                {
                    Data = allLanguage
                });
            }
            var filteredLanguage = await _service.GetLanguageByName(name);
            return Ok(new APIResponse<ReadLanguageDto>()
            {
                Data = filteredLanguage
            });

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLanguageById(string id)
        {
            var language = await _service.GetLanguageById(id);
            return Ok(new APIResponse<ReadLanguageDto>()
            {
                Data = language
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateLanguage([FromBody] AddLanguageDto addLanguageDto)
        {
            await _service.CreateLanguageAsync(addLanguageDto);
            return Ok(new APIResponse<string>()
            {
                Data = "Language Created Successfully"
            });
        }

    }
}