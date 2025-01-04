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
        public Task<string> GenerateJwtToken(Users user);
    }
}
