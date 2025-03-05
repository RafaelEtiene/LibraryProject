using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Data.Repositories;
using Library.Infrastructure.Data.Repositories.Interfaces;
using Library.Application.Services;
using Library.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using Library.Application.Mapper;

namespace Library.Test.DependencyInjection
{
    public static class DependencyInjection
    {
        public static ServiceProvider AddInfrastructureServices(this IServiceCollection services)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddDbContext<LibraryContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 34))));


            var configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<BookInfoProfile>();
                cfg.AddProfile<ClientProfile>();
                cfg.AddProfile<LoanProfile>();
            });

            services.AddSingleton(configurationMapper.CreateMapper());
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IMessageService, MessageService>();

            return services.BuildServiceProvider();
        }
    }
}
