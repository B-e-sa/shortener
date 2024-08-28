using Microsoft.EntityFrameworkCore;
using Shortener.Application;
using Shortener.Infrastructure;
using Shortener.Web;

var builder = WebApplication.CreateBuilder(args);

var presentationAssembly = typeof(Shortener.Presentation.AssemblyReference).Assembly;

builder.Services.AddEndpointsApiExplorer();

// Presentation layer & Controllers
builder.Services.AddControllers()
    .AddApplicationPart(presentationAssembly);

// Application Layer
builder.Services.AddApplicationServices();

builder.Services.AddDbContext<AppDbContext>(x =>
    {
        x.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
    },
    ServiceLifetime.Scoped
);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program;