using Library.Application.Services.Interfaces;
using Library.Core.Enum;
using Library.Application.Exceptions;
using Library.Core.Entities;
using Library.Infrastructure.Data.Repositories.Interfaces;

namespace Library.Application.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly IMessageService _messageService;

        public MonitoringService(IMonitoringRepository monitoringRepository, IMessageService messageService)
        {
            _monitoringRepository = monitoringRepository;
            _messageService = messageService;
        }

        public async Task JobMonitoringLoans()
        {
            await VerifyLoanAlmostDueAsync();

            await VerifyLoanDelayedAsync();
        }

        private async Task VerifyLoanAlmostDueAsync()
        {
            try
            {
                var result = await _monitoringRepository.ReturnLoanAlmostDue();

                if (result.Count() > 0)
                {
                    Console.WriteLine($"{result.Count()} with 4 days left until your loan is due.");
                    foreach (var item in result)
                    {
                        var dateComparison = item.IdStatusLoan == (int)StatusLoan.Renewed ? item.LastStatusDate : item.DateInitialLoan;
                        var dateIsDue = dateComparison.AddDays(14);

                        var subject = $"Hello {item.NameClient}! 4 days left until your loan is due";
                        var body = $"Your loan of the {item.NameBook} was made on day {item.DateInitialLoan.ToString("dd/MM")}. The deadline for you to renew or return ends on day {dateIsDue.ToString("dd/MM")}";

                        await _messageService.SendMessageAsync(item.PhoneNumber, body);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new BusinessException("An error ocurred in verify loans almost due.", ex);
            }            
        }

        private async Task VerifyLoanDelayedAsync()
        {
            try
            {
                var result = await _monitoringRepository.ReturnLoanDelayed();
                result = result.Where(e => e.PhoneNumber == "41997849659").ToList();

                if (result.Count() > 0)
                {
                    Console.WriteLine($"{result.Count()} late loans found.");
                    var idsLoans = result.Select(e => e.IdLoan).ToList();
                    await UpdateLateFineLoanAsync(idsLoans);

                    foreach (var item in result)
                    {
                        var body = $"Your loan of the {item.NameBook} is due. Actually the late fine is {item.LateFine.ToString("C2")}. \n Contact us to make payment and renew.";

                        await _messageService.SendMessageAsync(item.PhoneNumber, body);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error ocurred in verify loans delayed.{ex}");
            }
        }

        private async Task UpdateLateFineLoanAsync(IEnumerable<int> idLoans)
        {
            try
            {
                await _monitoringRepository.UpdateLateFineLoan(idLoans);
            }
            catch(Exception ex)
            {
                throw new BusinessException("An error ocurred in update late fine of the loans.", ex);
            }
        }
    }
}
