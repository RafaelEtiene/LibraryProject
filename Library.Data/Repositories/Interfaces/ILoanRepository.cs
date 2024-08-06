using Library.Model.Model;

namespace Library.Data.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        public Task<LoanInfo> GetBookLoanById(int idLoan);
        public Task<int> InsertNewBookLoan(Loan loan);
        public Task RenovateLoan(int idLoan);
        public Task FinishLoan(int idLoan);
        public Task<bool> ValidateExistLoan(int idClient);
    }
}
