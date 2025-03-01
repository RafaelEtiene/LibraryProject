using Library.Application.ViewModel;
using Library.Core.Entities;

namespace Library.Application.Services.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookInfoViewModel>> GetAllBooksAsync();
        public Task<int> InsertBookAsync(BookInfoViewModel book);
        public Task InsertMassiveBookAsync();
    }
}
