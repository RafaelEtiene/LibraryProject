using Library.Core.Entities;
using Library.Infrastructure.Models;

namespace Library.Infrastructure.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooksAsync();
        public Task<int> InsertBookAsync(BookInfo book);
        public Task InsertMassiveBooks();
    }
}
