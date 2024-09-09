using Microsoft.AspNetCore.Builder;
using Shortener.Application;
using Shortener.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shortener.Infrastructure;
using Shortener.Infrastructure.Data;
using Shortener.Web.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicies",
        x =>
        {
            x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();

// DI
var presentationAssembly = typeof(Shortener.Presentation.AssemblyReference).Assembly;
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddSwaggerGen();


builder.Services.ConfigureOptions<JwtSetup>();
builder.Services.ConfigureOptions<JwtBearerSetup>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.InitialiseDatabaseAsync();
}

app.UseCors("MyPolicies");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program;