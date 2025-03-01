using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Core.Entities;
using Library.Application.Validators;
using Library.Application.Services.Interfaces;
using AutoMapper;
using Library.Application.ViewModel;
using Library.Infrastructure.Data.Repositories.Interfaces;

namespace Library.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanInfoViewModel> GetBookLoanByIdAsync(int idLoan)
        {
            var loanInfo = await _loanRepository.GetBookLoanById(idLoan);

            if(loanInfo is null)
            {
                throw new BusinessException($"Not found loan info for this idLoan {idLoan}.");
            }

            return _mapper.Map<LoanInfoViewModel>(loanInfo);
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
