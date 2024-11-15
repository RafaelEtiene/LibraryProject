using Dapper;
using Library.Data.Repositories.Interfaces;
using Library.Model.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            var query = @"SELECT idUser IdUser, user User, password Password
                          FROM users;";

            using(var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryAsync<Users>(query);
            }
        }
    }
}
