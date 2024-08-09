using Dapper;
using Library.Data.Repositories.Interfaces;
using Library.Model.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LoanInfo>> ReturnLoanDelayed()
        {
            var query = @"SELECT l.idLoan as IdLoan, b.nameBook as NameBook, c.name as NameClient, l.dateInitialLoan as DateInitialLoan, l.idStatusLoan as IdStatusLoan, l.lateFine as LateFine, l.note as Note, c.email as Email 
                          FROM loan l
                          LEFT JOIN book b ON l.idBook = b.idBook 
                          LEFT JOIN client c ON l.idClient = c.idClient
                          WHERE (timestampdiff(day, l.dateInitialLoan, current_date()) > 14 AND l.idStatusLoan NOT IN (3, 4)) OR (l.idStatusLoan = 3 AND timestampdiff(day, l.lastStatusDate, current_date()) > 14)";

            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryAsync<LoanInfo>(query);
            }
        }

        public Task UpdateLateFineLoan(IEnumerable<LoanInfo> loans)
        {
            throw new NotImplementedException();
        }
    }
}
