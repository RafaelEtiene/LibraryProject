using Dapper;
using Library.Core.Entities;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Library.Infrastructure.Data.Repositories.Interfaces;

namespace Library.Infrastructure.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string connectionString;

        public ClientRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Client> GetClientByIdAsync(int idClient)
        {
            var query = @"SELECT idclient as IdClient, name as Name, age as Age, gender as Gender, email as Email, phonenumber as Phonenumber, address as Address
                          FROM client
                          WHERE idClient = @IdClient;";
            var param = new DynamicParameters();
            param.Add("@IdClient", idClient);

            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Client>(query, param);
            }
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            var query = @"SELECT idclient as IdClient, name as Name, age as Age, gender as Gender, email as Email, phonenumber as Phonenumber, address as Address
                          FROM client;";

            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryAsync<Client>(query);
            }
        }

        public async Task<int> InsertNewClientAsync(Client client)
        {
            var query = @"INSERT INTO client (name, age, gender, email, phonenumber, address)
                          VALUES (@Name, @Age, @Gender, @Email, @PhoneNumber, @Address);
                           SELECT last_insert_id();";

            var param = new DynamicParameters();
            param.Add("@Name", client.Name);
            param.Add("@Age", client.Age);
            param.Add("@Gender", client.Gender);
            param.Add("@Email", client.Email);
            param.Add("@PhoneNumber", client.PhoneNumber);
            param.Add("@Address", client.Address);

            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryFirstAsync<int>(query, param);
            }
        }
    }
}
