using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UserService.API.Extensions;
using UserService.Infrastructure.Identity;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    Console.WriteLine("Seeding roles...");
    await DbInitializer.SeedRolesAsync(roleManager);
    Console.WriteLine("Seeding complete.");
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

