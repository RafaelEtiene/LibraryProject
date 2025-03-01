using Dapper;
using Library.Infrastructure.Data.Repositories.Interfaces;
using Library.Core.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Library.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.Models;

namespace Library.Infrastructure.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string connectionString;
        private readonly LibraryContext _context;

        public BookRepository(IConfiguration configuration, LibraryContext libraryContext)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = libraryContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            using (var context = _context)
            {
                return await _context.Books.ToListAsync();
            }
        }

        public async Task<int> InsertBookAsync(BookInfo book)
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
