
using ConstrainService.Application.DTOs.Constrain;
using ConstrainService.Application.DTOs.Response;
using ConstrainService.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;


namespace ConstrainService.API.Controllers
{
    [Route("[controller]")]
    public class ConstrainController(ILogger<ConstrainController> logger, IConstrainServiceProvider constrainServiceProvider) : ControllerBase
    {
        private readonly ILogger<ConstrainController> _logger = logger;
        private readonly IConstrainServiceProvider _constrainServiceProvider = constrainServiceProvider;

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddConstrain(AddConstrainDto addConstrainDto)
        {
            await _constrainServiceProvider.AddConstrain(addConstrainDto);

            return Ok(new APIResponse<string>()
            {
                Data = "Constrain Created Successfully"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConstrains()
        {
            var constrains = await _constrainServiceProvider.GetAllConstrain();

            return Ok(new APIResponse<List<ReadConstrainDto>>()
            {
                Data = constrains
            });
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAllConstrains(string id)
        {
            var constrain = await _constrainServiceProvider.GetConstrainById(id);

            return Ok(new APIResponse<ReadConstrainDto>()
            {
                Data = constrain
            });
        }

    }
}