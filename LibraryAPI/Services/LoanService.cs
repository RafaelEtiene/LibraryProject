using LibraryAPI.Domain.DTO;
using LibraryAPI.Domain.Exceptions;
using LibraryAPI.Domain.Model;
using LibraryAPI.Domain.Validators;
using LibraryAPI.Repositories.Interfaces;
using LibraryAPI.Services.Interfaces;

namespace LibraryAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public Task<LoanInfo> GetBookLoanByIdAsync(int idLoan)
        {
            var loanInfo = _loanRepository.GetBookLoanById(idLoan);

            if(loanInfo is null)
            {
                throw new BusinessException($"Not found loan info for the idLoan {idLoan}.");
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

        public Task RenovateLoanAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task FinishLoanAsync(int idLoan)
        {
            throw new NotImplementedException();
        }
    }
}
