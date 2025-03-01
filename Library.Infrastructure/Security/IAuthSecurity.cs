using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.Interfaces
{
    public interface IAuthSecurity
    {
        public Task<string> GenerateJwtToken(Users user);
    }
}
