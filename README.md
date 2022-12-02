# Domain
Simple gRPC domain service written on C#

## Build

```
cd Domain
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:IncludeNativeLibrariesForSelfExtract=true
```

Artifacts will be in `../Domain/bin/Release/net6.0/linux-x64/publish`

## Instalation

1. Replace executable artifact (Domain) in /srv/domain
2. Grant rights to the directory and executable files to the user on whose behalf the service will be launched:

```
chown yourusername -R /srv/domain
chmod +x /srv/domain/Domain
```
3. Create file /etc/systemd/system/domain.service with content

```
[Unit]
Description=domain service

[Service]
# will set the Current Working Directory (CWD)
WorkingDirectory=/srv/domain
# systemd will run this executable to start the service
ExecStart=/srv/domain/Domain
# to query logs using journalctl, set a logical name here
SyslogIdentifier=Domain

# Use your username to keep things simple, for production scenario's I recommend a dedicated user/group.
# If you pick a different user, make sure dotnet and all permissions are set correctly to run the app.
# To update permissions, use 'chown yourusername -R /srv/AspNetSite' to take ownership of the folder and files,
#       Use 'chmod +x /srv/AspNetSite/AspNetSite' to allow execution of the executable file.
User=root

# ensure the service restarts after crashing
Restart=always
# amount of time to wait before restarting the service
RestartSec=5

# copied from dotnet documentation at
# https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-3.1#code-try-7
KillSignal=SIGINT
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

4. Execute `systemctl enable domain`
5. Execute `systemctl start domain`
6. Make sure that service is alive `systemctl status domain`

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

Sample Logstash config:
```
input {
	http {
		host => "localhost"
		port => 28080
		codec => json
	}
}

output {
	elasticsearch {
		hosts => ["https://localhost:9200"]
		index => "domain-logs"
		document_type => "log"
		ssl => true
		ssl_certificate_verification => false
		user => ""
		password => ""
	}
	stdout { }
}
```
With this sample config Logstash will write logs both in console and Elasticsearch.
