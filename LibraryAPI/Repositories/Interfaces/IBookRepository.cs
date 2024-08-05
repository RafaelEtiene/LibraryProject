using LibraryAPI.Domain.Model;

namespace LibraryAPI.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<BookInfo>> GetAllBooksAsync();
        public Task<int> InsertBook(BookInfo book);
        public Task InsertMassiveBooks();
    }
}
