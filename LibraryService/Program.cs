using Library.Infrastructure.Data.Repositories;
using Library.Infrastructure.Data.Repositories.Interfaces;
using Library.Application.Services;
using Library.Application.Services.Interfaces;
using Library.Service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IMonitoringService, MonitoringService>();
builder.Services.AddSingleton<IMonitoringRepository, MonitoringRepository>();
builder.Services.AddSingleton<IMessageService, MessageService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
