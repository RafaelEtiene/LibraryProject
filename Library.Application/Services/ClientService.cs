using Library.Application.Exceptions;
using Library.Core.Entities;
using Library.Application.Validators;
using Library.Application.Services.Interfaces;
using Library.Application.ViewModel;
using AutoMapper;
using Library.Infrastructure.Data.Repositories.Interfaces;

namespace Library.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientViewModel> GetClientByIdAsync(int idClient)
        {
            var client = await _clientRepository.GetClientByIdAsync(idClient);
            
            if(client is null)
            {
                throw new BusinessException($"Not found loan info for this idLoan {idClient}.");
            }

            return _mapper.Map<ClientViewModel>(client);
        }

        public async Task<IEnumerable<ClientViewModel>> GetClientsAsync()
        {
            var clients = await _clientRepository.GetClientsAsync();

            if (clients.Count() < 1)
            {
                throw new BusinessException($"No clients found.");
            }

            return _mapper.Map<IEnumerable<ClientViewModel>>(clients);
        }

        public async Task<int> InsertNewClientAsync(ClientViewModel clientViewModel)
        {
            var client = _mapper.Map<Client>(clientViewModel);

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
