using Microsoft.AspNetCore.Diagnostics;
using PlaygroundService.Infrastructure.Configurations.Database;
using PlaygroundService.Infrastructure.Configurations.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// app.UseMiddleware<DbTransactionMiddleware>(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/", () => "HackCode is working bitchessssss!");




app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseCors("AllowAny");
await app.Services.InitializeDbAsync();
app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var exceptionHandler = context.RequestServices.GetRequiredService<IExceptionHandler>();
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (!context.Response.HasStarted && exception != null)
        {
            context.Response.Clear(); // Ensure no partial response has been written
            context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Set appropriate status
            await exceptionHandler.TryHandleAsync(context, exception, context.RequestAborted);
        }
    });
});
app.Run();

