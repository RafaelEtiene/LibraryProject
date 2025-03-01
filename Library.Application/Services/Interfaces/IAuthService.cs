using Library.Application.ViewModel;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> GenerateJwtToken(UserViewModel user);
    }
}
