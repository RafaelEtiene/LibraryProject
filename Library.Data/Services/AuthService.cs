﻿using Library.Data.Repositories.Interfaces;
using Library.Data.Services.Interfaces;
using Library.Model.Exceptions;
using Library.Model.Model;
using Library.Model.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtToken(Users user)
        {
            var validator = new TokenValidator();
            var result = validator.Validate(user);

            if (!result.IsValid)
            {
                var propertiesError = validator.ReturnPropertiesWithError(result.Errors);

                throw new BusinessException($"The request is invalid. {propertiesError}");
            }

            if (await GetUsers(user.User, user.Password))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.User),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                throw new BusinessException("The password is incorrect.");
            }
        }

        private async Task<bool> GetUsers(string userName, string password)
        {
            var users = await _authRepository.GetUsers();
            var user = users.FirstOrDefault(u => u.User == userName);

            if (user is null)
            {
                throw new BusinessException("The user is not registered in the API");
            }

            return user.Password == password;
        }
    }
}
