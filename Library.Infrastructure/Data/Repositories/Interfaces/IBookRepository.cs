using Library.Core.Entities;

namespace Library.Infrastructure.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<BookInfo>> GetAllBooksAsync();
        public Task<int> InsertBookAsync(BookInfo book);
        public Task InsertMassiveBooks();
    }
}
