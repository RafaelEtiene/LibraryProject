using Library.Application.Services.Interfaces;
using Library.Application.Exceptions;
using Library.Core.Entities;
using Library.Application.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Library.Application.ViewModel;
using AutoMapper;
using Library.Infrastructure.Data.Repositories.Interfaces;

namespace Library.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthSecurity _authSecurity;
        private readonly IMapper _mapper;

        public AuthService(IAuthSecurity authSecurity, IMapper mapper)
        {
            _authSecurity = authSecurity;
            _mapper = mapper;
        }

        public async Task<string> GenerateJwtToken(UserViewModel request)
        {
            return await _authSecurity.GenerateJwtToken(_mapper.Map<Users>(request));
        }
    }
}
