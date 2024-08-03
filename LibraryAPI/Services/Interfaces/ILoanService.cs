using LibraryAPI.Domain.DTO;
using LibraryAPI.Domain.Model;

namespace LibraryAPI.Services.Interfaces
{
    public interface ILoanService
    {
        public Task<LoanInfo> GetBookLoanByIdAsync(int idLoan);
        public Task<int> InsertNewBookLoanAsync(LoanInsertDTO loan);
        public Task RenovateLoanAsync(Client client);
        public Task FinishLoanAsync(int idLoan);
    }
}
