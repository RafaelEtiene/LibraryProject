using Library.Data.Services.Interfaces;
using Library.Model.Model;

namespace Library.Data.Services
{
    public class MonitoringService : IMonitoringService
    {
        public Task<IEnumerable<LoanInfo>> ReturnLoanAlmostDue()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanInfo>> ReturnLoanDelayed()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateLateFineLoan(IEnumerable<LoanInfo> loans)
        {

        }
    }
}
