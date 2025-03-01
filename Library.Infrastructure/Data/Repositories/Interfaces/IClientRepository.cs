using Library.Core.Entities;

namespace Library.Infrastructure.Data.Repositories.Interfaces
{
    public interface IClientRepository
    {
        public Task<IEnumerable<Client>> GetClientsAsync();
        public Task<Client> GetClientByIdAsync(int idClient);
        public Task<int> InsertNewClientAsync(Client client);
    }
}
