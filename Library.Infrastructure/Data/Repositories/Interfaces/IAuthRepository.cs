using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        public Task<IEnumerable<Users>> GetUsers();
    }
}
