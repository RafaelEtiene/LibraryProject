using Library.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Services.Interfaces
{
    public interface IAuthService
    {
        private Task<bool> GetUsers(string userName, string password);

        public Task<string> GenerateJwtToken(string user, string password);
    }
}
