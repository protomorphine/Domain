using Domain.Core.Mapper;
using Domain.Extensions;
using Domain.Infrastructure.Data;
using Domain.Interceptors;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

builder.Configuration.AddConfiguration(config);

builder.Host.UseSerilog(logger);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ApiKeyInterceptor>();
});

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
var dbOptions = appSettings.DbOptions;
var apiKeyOptions = appSettings.ApiKeyOptions;

builder.Services.AddScoped(_ => appSettings)
    .AddScoped(_ => dbOptions)
    .AddScoped(_ => apiKeyOptions);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(dbOptions.ConnectionString);
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddRepositories();

builder.Services.AddAutoMapper(typeof(DefaultProfile));

var app = builder.Build();

app.AddGrpcServices();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();