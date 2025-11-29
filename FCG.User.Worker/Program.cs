using FCG.User.Worker;
using FCG.User.Infra.Data.Messaging.Config;
using FCG.User.Infra.Data;
using FCG.User.Application;
using System.Net;

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

#region RabbitMq
var rabbitSection = builder.Configuration.GetSection("RabbitMq");

var rabbitSettingsSection = rabbitSection.GetSection("Settings");
if (!rabbitSettingsSection.Exists())
    throw new InvalidOperationException("Section 'RabbitMqSettings' not found in configuration.");
builder.Services.Configure<RabbitMqOptions>(rabbitSettingsSection);

var queuesSection = rabbitSection.GetSection("Queues");
if (!queuesSection.Exists())
    throw new InvalidOperationException("Section 'Queues' not found in configuration.");
builder.Services.Configure<QueuesOptions>(queuesSection);

builder.Services.ConfigureRabbitMq();
#endregion

builder.Services.ConfigureServices();

var host = builder.Build();
host.Run();
