using Microsoft.EntityFrameworkCore;
using Shortener.Data;
using Shortener.Data.Repositories;
using Shortener.Services.Implementations;
using Shortener.Services.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(x =>
    {
        x.SuppressMapClientErrors = true;
    });
builder.Services.AddDbContext<AppDbContext>(x =>
    {
        x.UseSqlite(builder.Configuration.GetConnectionString("Database"));
    },
    ServiceLifetime.Scoped
);
builder.Services.AddSwaggerGen();

// SERVICES
builder.Services.AddScoped<ICreateUrlService, CreateUrlService>();
builder.Services.AddScoped<IFindByShortUrlService, FindByShortUrlService>();
builder.Services.AddScoped<IGetTopUrlsService, GetTopUrlsService>();

// REPOSITORIES
builder.Services.AddScoped<IUrlRepository, UrlRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();