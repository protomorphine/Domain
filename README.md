# Domain
Simple gRPC domain service written on C#

## Database

For example in this project uses sqlite databae.

You can use any database, supporteg by Entity Framework. 

Just update `Program.cs` file in Domain project
```
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(dbOptions.ConnectionString);
    options.UseSnakeCaseNamingConvention();
});
```

## Logstash

To write logs to Logstash update `appsettings.json` like this:
```
    "WriteTo": [
      {
        "Name": "Http",
        "Args": {
          "requestUri": "http://localhost:28080", // Logstash uri
          "queueLimitBytes": null
        }
      }
```
