using Dapper;
using Library.Infrastructure.Data.Repositories.Interfaces;
using Library.Core.Enum;
using Library.Core.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Library.Infrastructure.Data.Repositories
{
    public class MonitoringRepository : IMonitoringRepository
    {
        private readonly string connectionString;

        public MonitoringRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<LoanInfo>> ReturnLoanAlmostDue()
        {
            var query = @"SELECT l.idLoan as IdLoan, b.nameBook as NameBook, c.name as NameClient, l.dateInitialLoan as DateInitialLoan, l.idStatusLoan as IdStatusLoan, l.lateFine as LateFine, l.note as Note, c.email as Email 
                          FROM loan l
                          LEFT JOIN book b ON l.idBook = b.idBook 
                          LEFT JOIN client c ON l.idClient = c.idClient
                          WHERE (timestampdiff(day, l.dateInitialLoan, current_date()) <= 10 AND timestampdiff(day, l.dateInitialLoan, current_date()) > 8 AND l.idStatusLoan NOT IN (3, 4)) OR (l.idStatusLoan = 3 AND timestampdiff(day, l.lastStatusDate, current_date()) <= 10 AND timestampdiff(day, l.lastStatusDate, current_date()) > 8)";

            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryAsync<LoanInfo>(query);
            }
        }

        public async Task<IEnumerable<LoanInfo>> ReturnLoanDelayed()
        {
            var query = @"SELECT l.idLoan as IdLoan, b.nameBook as NameBook, c.name as NameClient, l.dateInitialLoan as DateInitialLoan, l.idStatusLoan as IdStatusLoan, l.lateFine as LateFine, l.note as Note, c.email as Email, l.lastStatusDate as LastStatusDate 
                          FROM loan l
                          LEFT JOIN book b ON l.idBook = b.idBook 
                          LEFT JOIN client c ON l.idClient = c.idClient
                          WHERE (timestampdiff(day, l.dateInitialLoan, current_date()) > 14 AND l.idStatusLoan NOT IN (3, 4)) OR (l.idStatusLoan = 3 AND timestampdiff(day, l.lastStatusDate, current_date()) > 14)";

            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryAsync<LoanInfo>(query);
            }
        }

        public async Task UpdateLateFineLoan(IEnumerable<int> idsLoan)
        {
            var query = @"UPDATE loan SET lateFine = lateFine + @LateFine, idStatusLoan = @IdStatusLoan, lastStatusDate = NOW()
                          WHERE idLoan IN @IdsLoan;";

            var param = new DynamicParameters();
            param.Add("@LateFine", 1);
            param.Add("@IdStatusLoan", (int)StatusLoan.Delayed);
            param.Add("@IdsLoan", idsLoan);

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, param);
            }
        }
    }
}
