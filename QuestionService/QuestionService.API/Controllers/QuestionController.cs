using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionService.Application.DTOs.QuestionDto;
using QuestionService.Application.DTOs.ResponseDto;
using QuestionService.Application.Interfaces.Service;

namespace QuestionService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
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

    [HttpGet]
    [Route("abstract")]
    public async Task<IActionResult> GetAbstractQuestionById()
    {
        var question = await _questionService.GetAllAbstractQuestion();
        return Ok(new APIResponse<List<ReadAbstractQuestionDto>>()
        {
            Data = question
        });
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetFullQuestoinByID(string id)
    {
        var question = await _questionService.GetFullQuestionById(id);
        return Ok(new APIResponse<ReadQuestionDto>()
        {
            Data = question
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetFullQuestions()
    {
        var question = await _questionService.GetFullQuestions();
        return Ok(new APIResponse<List<ReadQuestionDto>>()
        {
            Data = question
        });
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteQuestion(string id)
    {
        await _questionService.DeleteQuestion(id);
        return Ok(new APIResponse<string>()
        {
            Data = "Question Deleted successfully"
        });
    }

    [Authorize]
    [HttpDelete]
    [Route("permanent/{id}")]
    public async Task<IActionResult> DeletePermanently(string id)
    {
        await _questionService.DeleteQuestionPermanently(id);
        return Ok(new APIResponse<string>()
        {
            Data = "Question Deleted Permanently"
        });
    }


}
