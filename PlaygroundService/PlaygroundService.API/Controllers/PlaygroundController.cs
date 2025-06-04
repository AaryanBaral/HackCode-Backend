using Microsoft.AspNetCore.Mvc;
using PlaygroundService.Application.Interfaces.Playground;

namespace  PlaygroundService.API.Controllers;
[Route("api/[controller]")]
public class PlaygroundController(IPlaygroundService playgroundService) : ControllerBase
{
    private readonly IPlaygroundService _playgroundService = playgroundService;

    [HttpGet]
    [Route("get-language")]
    public async Task<IActionResult> GetLanguage([FromQuery]string name)
    {
        var result = await _playgroundService.GetLanguage(name);
        return Ok(result);
    }

    [HttpGet]
    [Route("test-kafka")]
    public async Task<IActionResult> TestKafka()
    {
        await _playgroundService.TestKafka();
        return Ok();
    }
}