using Library.Infrastructure.Models;

namespace Library.Infrastructure.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        public Task<IEnumerable<User>> GetUsers();
    }
}
