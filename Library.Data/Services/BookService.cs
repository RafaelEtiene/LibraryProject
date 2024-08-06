using Library.Model.Exceptions;
using Library.Model.Model;
using Library.Model.Validators;
using Library.Data.Repositories.Interfaces;
using Library.Data.Services.Interfaces;

namespace Library.Data.Services
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

        public async Task<int> InsertBookAsync(BookInfo book)
        {
            var validator = new BookValidator();
            var result = validator.Validate(book);

            if (!result.IsValid)
            {
                var propertiesError = validator.ReturnPropertiesWithError(result.Errors);

                throw new BusinessException($"The request is invalid. {propertiesError}");
            }

            return await _bookRepository.InsertBook(book);
        }

        public Task InsertMassiveBookAsync()
        {
            throw new NotImplementedException();
        }
    }
}
