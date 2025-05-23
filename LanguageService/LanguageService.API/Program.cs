using LanguageService.API.Extensions;
using LanguageService.Infrastructure.Configurations.Database;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddAppServices(builder.Configuration, builder.Environment);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


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


app.UseCors("AllowAny");
app.UseHttpsRedirection();


app.MapGet("/", () => "HackCode is working bitchessssss!");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


