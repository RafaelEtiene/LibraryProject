using Library.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories.Interfaces
{
    public interface IMonitoringRepository
    {
        public Task<IEnumerable<LoanInfo>> ReturnLoanDelayed();
        public Task<IEnumerable<LoanInfo>> ReturnLoanAlmostDue();
        public Task UpdateLateFineLoan(IEnumerable<LoanInfo> loans);
    }
}
