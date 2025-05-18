using Microsoft.AspNetCore.Diagnostics;
using QuestionService.Infrastructure.Configuration.Database;
using QuestionService.API.Extensions;
using QuestionService.API.Middlewares;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApiServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAny");
app.UseHttpsRedirection();


app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var exceptionHandler = context.RequestServices.GetRequiredService<IExceptionHandler>();
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception != null && !context.Response.HasStarted)
        {
            context.Response.Clear();
            await exceptionHandler.TryHandleAsync(context, exception, context.RequestAborted);
        }
        else if (context.Response.HasStarted)
        {
            // Log that response has started
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("Response already started, skipping exception handling");
        }
    });
});

app.UseMiddleware<DbTransactionMiddleware>();

app.MapGet("/", () => "HackCode is working bitchessssss!");
app.UseAuthentication();
app.UseAuthorization();

await app.Services.InitializeDbAsync();
app.MapControllers();



app.Run();
