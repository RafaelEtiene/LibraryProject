using LibraryAPI.Domain.Model;

namespace LibraryAPI.Services.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookInfo>> GetAllBooksAsync();
        public Task InsertBook(BookInfo book);
        public Task InsertMassiveBooks();
    }
}
