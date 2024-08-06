using Library.Model.DTO;
using Library.Model.Model;

namespace Library.Data.Services.Interfaces
{
    public interface ILoanService
    {
        public Task<LoanInfo> GetBookLoanByIdAsync(int idLoan);
        public Task<int> InsertNewBookLoanAsync(LoanInsertDTO loan);
        public Task RenovateLoanAsync(int idLoan);
        public Task FinishLoanAsync(int idLoan);
    }
}
