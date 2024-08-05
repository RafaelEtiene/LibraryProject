using FluentValidation;
using FluentValidation.Results;
using LibraryAPI.Domain.Model;
using System.Text;

namespace LibraryAPI.Domain.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(client => client.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(client => client.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(client => client.Age).NotEmpty().WithMessage("Age is required.");
            RuleFor(client => client.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(client => client.Gender).NotEmpty().WithMessage("Gender is required.");
            RuleFor(client => client.PhoneNumber).NotEmpty().WithMessage("Phonenumber is required.");
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
