using LibraryAPI.Domain.Exceptions;
using LibraryAPI.Domain.Model;
using LibraryAPI.Domain.Validators;
using LibraryAPI.Repositories.Interfaces;
using LibraryAPI.Services.Interfaces;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookInfo>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();

            if(books.Count() < 1)
            {
                throw new BusinessException("No registered books found.");
            }

            return books;
        }

        public async Task InsertBook(BookInfo book)
        {
            var validator = new BookValidator();
            var result = validator.Validate(book);

            if (!result.IsValid)
            {
                var propertiesError = validator.ReturnPropertiesWithError(result.Errors);

                throw new BusinessException($"The request is invalid. {propertiesError}");
            }

            await _bookRepository.InsertBook(book);
        }

        public Task InsertMassiveBooks()
        {
            throw new NotImplementedException();
        }
    }
}
