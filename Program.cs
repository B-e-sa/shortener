using Microsoft.EntityFrameworkCore;
using Shortener.Application;
using Shortener.Application.Commands.Url.CreateUrl;
using Shortener.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var presentationAssembly = typeof(Shortener.Presentation.AssemblyReference).Assembly;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
    .AddApplicationPart(presentationAssembly)
    .ConfigureApiBehaviorOptions(x =>
    {
        x.SuppressMapClientErrors = true;
        x.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddApplicationServices();

builder.Services.AddDbContext<AppDbContext>(x =>
    {
        x.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
    },
    ServiceLifetime.Scoped
);

builder.Services.AddSwaggerGen();

// SERVICES
builder.Services.AddScoped<CreateUrlCommand>();
// builder.Services.AddScoped<FindUrlByShortUrlCommand>();
// builder.Services.AddScoped<GetTopUrlsCommand>();
// builder.Services.AddScoped<DeleteUrlCommand>();
// builder.Services.AddScoped<FindUrlByIdCommand>();
// builder.Services.AddScoped<VisitUrlCommand>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program;