using LibraryAPI.Domain.Model;

namespace LibraryAPI.Services.Interfaces
{
    public interface ILoanService
    {
        public Task<int> GetBookLoanById(int idLoan);
        public Task<int> InsertNewBookLoan(LoanInfo loan);
        public Task RenovateLoan(Client client);
        public Task FinishLoan(int idLoan);
    }
}
