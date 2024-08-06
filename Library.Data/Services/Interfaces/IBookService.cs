using Library.Model.Model;

namespace Library.Data.Services.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookInfo>> GetAllBooksAsync();
        public Task<int> InsertBookAsync(BookInfo book);
        public Task InsertMassiveBookAsync();
    }
}
