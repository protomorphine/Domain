using Domain.Core.Mapper;
using Domain.Core.Services;
using Domain.Extensions;
using Domain.Infrastructure.Data;
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

builder
    .Configuration.AddConfiguration(config);

builder.Host.UseSerilog(logger);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
var dbOptions = appSettings.DbOptions;

builder.Services.AddScoped(_ => appSettings);
builder.Services.AddScoped(_ => dbOptions);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(dbOptions.ConnectionString);
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddRepositories();

builder.Services.AddAutoMapper(typeof(DefaultProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGrpcService<ExampleService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();