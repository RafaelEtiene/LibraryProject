using Library.Application.Exceptions;
using Library.Core.Entities;
using Library.Application.Validators;
using Library.Application.Services.Interfaces;
using Library.Application.ViewModel;
using AutoMapper;
using Library.Infrastructure.Data.Repositories.Interfaces;

namespace Library.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookInfoViewModel>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();

            if(!books.Any())
            {
                throw new BusinessException("No registered books found.");
            }

            return _mapper.Map<IEnumerable<BookInfoViewModel>>(books);
        }

        public async Task<int> InsertBookAsync(BookInfoViewModel bookInfoViewModel)
        {
            var book = _mapper.Map<BookInfo>(bookInfoViewModel);

            var validator = new BookValidator();
            var result = validator.Validate(book);

            if (!result.IsValid)
            {
                var propertiesError = validator.ReturnPropertiesWithError(result.Errors);

                throw new BusinessException($"The request is invalid. {propertiesError}");
            }

            return await _bookRepository.InsertBookAsync(book);
        }

        public Task InsertMassiveBookAsync()
        {
            throw new NotImplementedException();
        }
    }
}
