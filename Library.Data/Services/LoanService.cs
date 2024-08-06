using Library.Model.DTO;
using Library.Model.Exceptions;
using Library.Model.Model;
using Library.Model.Validators;
using Library.Data.Repositories.Interfaces;
using Library.Data.Services.Interfaces;

namespace Library.Data.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<LoanInfo> GetBookLoanByIdAsync(int idLoan)
        {
            var loanInfo = await _loanRepository.GetBookLoanById(idLoan);

            if(loanInfo is null)
            {
                throw new BusinessException($"Not found loan info for this idLoan {idLoan}.");
            }

            return loanInfo;
        }

        public async Task<int> InsertNewBookLoanAsync(LoanInsertDTO loanInsert)
        {
            var validator = new LoanValidator();
            var result = validator.Validate(loanInsert);

            if (!result.IsValid)
            {
                var propertiesError = validator.ReturnPropertiesWithError(result.Errors);
                throw new BusinessException($"The request is invalid. {propertiesError}");
            }

            var activeLoanClient = await _loanRepository.ValidateExistLoan(loanInsert.IdClient);

            if (activeLoanClient)
            {
                throw new BusinessException($"The client already has an active loan.");
            }

            var loan = new Loan()
            {
                IdClient = loanInsert.IdClient,
                IdBook = loanInsert.IdBook,
                DateInitialLoan = DateTime.Now,
                LateFine = 0,
                IdStatusLoan = 1,
                Note = "New loan made."
            };

            return await _loanRepository.InsertNewBookLoan(loan);
        }

        public async Task RenovateLoanAsync(int idLoan)
        {
            await _loanRepository.RenovateLoan(idLoan);
        }

        public async Task FinishLoanAsync(int idLoan)
        {
            await _loanRepository.FinishLoan(idLoan);
        }
    }
}
