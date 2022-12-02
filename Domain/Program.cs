using Domain.Core.Exceptions;
using Domain.Core.Mapper;
using Domain.Extensions;
using Domain.Infrastructure.Data;
using Domain.Interceptors;
using Domain.Models;
using Grpc.Core;
using GrpcExceptionsMapping.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics.Tracing;

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

builder.Services.AddGrpcExceptionMapping(options =>
{
    options.Map<ObjectNotFoundException>(StatusCode.NotFound);
    options.Map<NotImplementedException>(StatusCode.Unimplemented);
    options.Map<DbUpdateConcurrencyException>(StatusCode.Cancelled);
});

builder.Services.UseGrpcExceptionMapping();

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