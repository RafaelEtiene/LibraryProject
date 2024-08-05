﻿using LibraryAPI.Domain.Model;

namespace LibraryAPI.Repositories.Interfaces
{
    public interface IClientRepository
    {
        public Task<IEnumerable<Client>> GetClientsAsync();
        public Task<Client> GetClientByIdAsync(int idClient);
        public Task<int> InsertNewClientAsync(Client client);
    }
}