using FluentValidation;
using FluentValidation.Results;
using LibraryAPI.Domain.Model;
using System.Text;

namespace LibraryAPI.Domain.Validators
{
    public class BookValidator : AbstractValidator<BookInfo>
    {
        public BookValidator()
        {
            RuleFor(book => book.NameBook).Length(2, 100).WithMessage("Title must be between 2 and 100 characters.")
                                          .NotEmpty().WithMessage("NameBook is required.");

            RuleFor(book => book.Author).NotEmpty().WithMessage("Author is required."); 
             ;
            RuleFor(book => book.PublicationDate).LessThanOrEqualTo(DateTime.Today).WithMessage("Published date cannot be in the future.")
                                                 .NotEmpty().WithMessage("PublicationDate is required.");

            RuleFor(book => book.Genre).NotEmpty().WithMessage("Genre is required."); ;
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
