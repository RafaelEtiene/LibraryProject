using Library.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Models;

namespace Library.Infrastructure.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LibraryContext _context;

        public AuthRepository(LibraryContext libraryContext)
        {
            _context = libraryContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            using (var context = _context)
            {
                return await context.Users.ToListAsync();
            }
        }
    }
}
