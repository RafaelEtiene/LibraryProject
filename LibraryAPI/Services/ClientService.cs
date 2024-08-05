using LibraryAPI.Domain.Exceptions;
using LibraryAPI.Domain.Model;
using LibraryAPI.Domain.Validators;
using LibraryAPI.Repositories.Interfaces;
using LibraryAPI.Services.Interfaces;

namespace LibraryAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> GetClientByIdAsync(int idClient)
        {
            var client = await _clientRepository.GetClientByIdAsync(idClient);
            
            if(client is null)
            {
                throw new BusinessException($"Not found loan info for this idLoan {idClient}.");
            }

            return client;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            var clients = await _clientRepository.GetClientsAsync();

            if (clients is null)
            {
                throw new BusinessException($"No clients found.");
            }

            return clients;
        }

        public async Task<int> InsertNewClientAsync(Client client)
        {
            var validator = new ClientValidator();
            var result = validator.Validate(client);

            if (!result.IsValid)
            {
                var propertiesError = validator.ReturnPropertiesWithError(result.Errors);

                throw new BusinessException($"The request is invalid. {propertiesError}");
            }

            return await _clientRepository.InsertNewClientAsync(client);
        }
    }
}
