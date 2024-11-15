using Library.Data.Repositories.Interfaces;
using Library.Data.Services.Interfaces;
using Library.Model.Enum;
using Library.Model.Exceptions;
using Library.Model.Model;
using Org.BouncyCastle.Ocsp;

namespace Library.Data.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly IEmailService _emailService;

        public MonitoringService(IMonitoringRepository monitoringRepository, IEmailService emailService)
        {
            _monitoringRepository = monitoringRepository;
            _emailService = emailService;
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
                    foreach (var item in result)
                    {
                        var dateComparison = item.IdStatusLoan == (int)StatusLoan.Renewed ? item.LastStatusDate : item.DateInitialLoan;
                        var dateIsDue = dateComparison.AddDays(14);

                        var subject = $"Hello {item.NameClient}! 4 days left until your loan is due";
                        var body = $"Your loan of the {item.NameBook} was made on day {item.DateInitialLoan.ToString("dd/MM")}. The deadline for you to renew or return ends on day {dateIsDue.ToString("dd/MM")}";

                        _emailService.SendEmailAsync(item.Email, subject, body);
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

                if (result.Count() > 0)
                {
                    var idsLoans = result.Select(e => e.IdLoan).ToList();
                    await UpdateLateFineLoanAsync(idsLoans);

                    foreach (var item in result)
                    {
                        var subject = $"Hello {item.NameClient}! Your loan is due.";
                        var body = $"Your loan of the {item.NameBook} is due. Actually the late fine is R${item.LateFine.ToString("C2")}";


                        await _emailService.SendEmailAsync(item.Email, subject, body);
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
