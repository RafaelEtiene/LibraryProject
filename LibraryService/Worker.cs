using Library.Application.Services.Interfaces;

namespace Library.Service
{
    public class Worker : BackgroundService
    {
        private readonly IMonitoringService _monitoringService;
        private const int _oneDayDelay = 180000;
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

                await Task.Delay(_oneDayDelay, stoppingToken);
            }
        }
    }
}
