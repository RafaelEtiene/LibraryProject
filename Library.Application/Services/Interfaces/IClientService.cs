using Library.Application.ViewModel;
using Library.Core.Entities;

namespace Library.Application.Services.Interfaces
{
    public interface IClientService
    {
        public Task<IEnumerable<ClientViewModel>> GetClientsAsync(); 
        public Task<ClientViewModel> GetClientByIdAsync(int idClient); 
        public Task<int> InsertNewClientAsync(ClientViewModel client); 
    }
}
