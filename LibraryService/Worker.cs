using Library.Application.Services.Interfaces;

namespace Library.Service
{
    public class Worker : BackgroundService
    {
        private readonly IMonitoringService _monitoringService;
        public Worker(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"LibraryService runnig at {DateTime.Now}");

                await _monitoringService.JobMonitoringLoans();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
