using LibraryAPI.Domain.Model;

namespace LibraryAPI.Services.Interfaces
{
    public interface IClientService
    {
        public Task<IEnumerable<Client>> GetClientsAsync(); 
        public Task<Client> GetClientByIdAsync(int idClient); 
        public Task<int> InsertNewClientAsync(Client client); 
    }
}
