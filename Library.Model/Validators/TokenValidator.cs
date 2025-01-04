using FluentValidation;
using FluentValidation.Results;
using Library.Model.Model;
using System.Text;

namespace Library.Model.Validators
{
    public class TokenValidator : AbstractValidator<Users>
    {
        public TokenValidator()
        {
            RuleFor(user => user.User).Length(2, 100).NotEmpty().WithMessage("User is required.");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.");
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
