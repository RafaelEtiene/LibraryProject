using Dapper;
using Library.Data.Repositories.Interfaces;
using Library.Model.Enum;
using Library.Model.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Library.Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly string connectionString;

        public LoanRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task FinishLoan(int idLoan)
        {
            var query = @"UPDATE loan
                        SET idStatusLoan = @IdStatusLoan, note = @Note, lastStatusDate = @LastStatusDate
                        WHERE idLoan = @IdLoan";

            var message = "The book was returned.";

            var param = new DynamicParameters();
            param.Add("@IdStatusLoan", (int)StatusLoan.Returned);
            param.Add("@Note", message);
            param.Add("@IdLoan", idLoan);
            param.Add("@LastStatusDate", DateTime.Now);

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task<LoanInfo> GetBookLoanById(int idLoan)
        {
            var query = @"SELECT l.idLoan as IdLoan, b.nameBook as NameBook, c.name as NameClient, l.dateInitialLoan as DateInitialLoan, l.idStatusLoan as IdStatusLoan, l.lateFine as LateFine, l.note as Note, c.email as Email 
                          FROM loan l LEFT JOIN book b ON l.idBook = b.idBook LEFT JOIN client c ON l.idClient = c.idClient  
                          WHERE idLoan = @IdLoan";

            var param = new DynamicParameters();
            param.Add("@IdLoan", idLoan);

            using (var connection = new MySqlConnection(connectionString))
            {
               return await connection.QuerySingleOrDefaultAsync<LoanInfo>(query, param);
            }
        }

        public async Task<int> InsertNewBookLoan(Loan loan)
        {
            var query = @"INSERT INTO loan (idLoan, idBook, idClient, dateInitialLoan, idStatusLoan, lateFine, note)
                          VALUES (NULL, @IdBook, @IdClient, @DateInitialLoan, @IdStatusLoan, @LateFine, @Note);
                           SELECT last_insert_id();";

            var param = new DynamicParameters();
            param.Add("@IdBook", loan.IdBook);
            param.Add("@IdClient", loan.IdClient);
            param.Add("@DateInitialLoan", DateTime.Now);
            param.Add("@IdStatusLoan", loan.IdStatusLoan);
            param.Add("@LateFine", loan.LateFine);
            param.Add("@Note", loan.Note);

            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryFirstAsync<int>(query, param);
            }
        }

        public async Task RenovateLoan(int idLoan)
        {
            var query = @"UPDATE loan
                        SET idStatusLoan = @IdStatusLoan, note = @Note, lastStatusDate = @LastStatusDate
                        WHERE idLoan = @IdLoan;";

            var message = "The book was renewed.";

            var param = new DynamicParameters();
            param.Add("@IdStatusLoan", StatusLoan.Renewed);
            param.Add("@Note", message);
            param.Add("@IdLoan", idLoan);
            param.Add("@@LastStatusDate", DateTime.Now);

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task<bool> ValidateExistLoan(int idClient)
        {
            var query = @"SELECT EXISTS(SELECT 1 FROM loan
                          WHERE idClient = @IdClient AND idStatusLoan in (1, 2, 3));";

            var param = new DynamicParameters();
            param.Add("@IdClient", idClient);

            using(var connection = new MySqlConnection(connectionString))
            {
                return await connection.ExecuteScalarAsync<bool>(query, param);
            }
            
        }
    }
}
