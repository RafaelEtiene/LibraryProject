using Library.Application.DTO;
using Library.Application.ViewModel;
using Library.Core.Entities;

namespace Library.Application.Services.Interfaces
{
    public interface ILoanService
    {
        public Task<IEnumerable<LoanInfoViewModel>> GetAllLoans();
        public Task<LoanInfoViewModel> GetBookLoanByIdAsync(int idLoan);
        public Task<int> InsertNewBookLoanAsync(LoanInsertDTO loan);
        public Task RenovateLoanAsync(int idLoan);
        public Task FinishLoanAsync(int idLoan);
    }
}
