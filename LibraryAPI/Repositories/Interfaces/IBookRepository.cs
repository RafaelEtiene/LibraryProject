using LibraryAPI.Domain.Model;

namespace LibraryAPI.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<BookInfo>> GetAllBooksAsync();
        public Task InsertBook(BookInfo book);
        public Task InsertMassiveBooks();
    }
}
