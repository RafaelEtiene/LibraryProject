using Library.Data.Repositories;
using Library.Data.Repositories.Interfaces;
using Library.Data.Services;
using Library.Data.Services.Interfaces;
using LibraryService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<IMonitoringService, MonitoringService>();
builder.Services.AddTransient<IMonitoringRepository, MonitoringRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
