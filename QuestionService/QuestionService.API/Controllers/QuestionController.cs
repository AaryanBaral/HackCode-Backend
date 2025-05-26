using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionService.Application.DTOs.QuestionDto;
using QuestionService.Application.DTOs.ResponseDto;
using QuestionService.Application.Interfaces;
using QuestionService.Infrastructure.Persistence;

namespace QuestionService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IQuestionService _questionService;
    private readonly IConfiguration _configuration;

    public QuestionController(AppDbContext context, IQuestionService questionService, IConfiguration Configuration)
    {
        _context = context;
        _questionService = questionService;
        _configuration = Configuration;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddQuestion(AddQuestionDto addQuestionDto)
    {
        var userId = User.FindFirst("UserId")?.Value ?? throw new NullReferenceException("Token is not Valid");
        await _questionService.AddQuestionAsync(addQuestionDto, userId);
        return Ok(new APIResponse<string>()
        {
            Data = "Question Addded successfully"
        });
    }

    [HttpPost]
    [Route("kafka-test")]
    public async Task<IActionResult> TestKafka()
    {
        await _questionService.TestKafka();
        return Ok("check the consumers console");
    }


    [Authorize]
    [HttpGet]
    [Route("protected")]
    public IActionResult ProtectedRoute()
    {
        return Ok("you are authorized.");
    }

    [HttpPatch]
    [Route("update")]
    public async Task<IActionResult> UpdateQuestion([FromQuery] string id, [FromBody] UpdateQuestionDto updateQuestionDto)
    {
        await _questionService.UpdateQuestion(updateQuestionDto, id);
        return Ok(new APIResponse<string>()
        {
            Data = "Question Updated successfully"
        });
    }

}
