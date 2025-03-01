using FluentValidation;
using FluentValidation.Results;
using Library.Application.DTO;
using System.Text;

namespace Library.Application.Validators
{
    public class LoanValidator : AbstractValidator<LoanInsertDTO>
    {
        public LoanValidator()
        {
            RuleFor(loan => loan.IdBook).NotEmpty().WithMessage("Idloan is required.");

            RuleFor(loan => loan.IdClient).NotEmpty().WithMessage("Author is required.");
        }

        public string ReturnPropertiesWithError(List<ValidationFailure> errors)
        {
            var sb = new StringBuilder();

            foreach (var failure in errors)
            {
                sb.AppendLine($"Propertie: {failure.PropertyName} - Error: {failure.ErrorMessage}");
            }

            return sb.ToString();
        }
    }
}
