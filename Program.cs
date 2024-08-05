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
        x.SuppressModelStateInvalidFilter = true;
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
builder.Services.AddScoped<IFindUrlByShortUrlService, FindUrlByShortUrlService>();
builder.Services.AddScoped<IGetTopUrlsService, GetTopUrlsService>();
builder.Services.AddScoped<IDeleteUrlService, DeleteUrlService>();
builder.Services.AddScoped<IFindUrlByIdService, FindUrlByIdService>();
builder.Services.AddScoped<IVisitUrlService, VisitUrlService>();

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