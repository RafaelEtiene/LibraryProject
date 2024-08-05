using Dapper;
using LibraryAPI.Domain.Model;
using LibraryAPI.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace LibraryAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string connectionString;

        public BookRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<BookInfo>> GetAllBooksAsync()
        {
            var query = @"SELECT idBook as IdBook, nameBook as NameBook, author as Author, publicationDate as PublicationDate, idBookGenre as Genre
                          FROM Book";

            using(var connection = new MySqlConnection(connectionString))
            {            
               return await connection.QueryAsync<BookInfo>(query);
            }
        }

        public async Task<int> InsertBook(BookInfo book)
        {
            var query = @"INSERT INTO Book (idBook, nameBook, author, publicationDate, idBookGenre) 
                          VALUES (null, @NameBook, @Author, @PublicationDate, @IdBookGenre);
                          SELECT last_insert_id();";

            var param = new DynamicParameters();
            param.Add("@NameBook", book.NameBook);
            param.Add("@Author", book.Author);
            param.Add("@PublicationDate", book.PublicationDate);
            param.Add("@IdBookGenre", (int)book.Genre);

            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryFirstAsync<int>(query, param);
            }
        }

        public Task InsertMassiveBooks()
        {
            throw new NotImplementedException();
        }
    }
}
