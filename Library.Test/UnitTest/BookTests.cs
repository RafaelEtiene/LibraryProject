using Bogus;
using Library.Application.Services;
using Library.Application.Services.Interfaces;
using Library.Application.ViewModel;
using Library.Core.Entities;
using Library.Core.Enum;
using Library.Infrastructure.Data.Repositories;
using Library.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Library.Test.DependencyInjection.DependencyInjection;
using System;
using Library.Test.DependencyInjection;

namespace Library.Test.UnitTest
{
    public class BookTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IBookService _bookService;

        public BookTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddInfrastructureServices();
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _bookService = _serviceProvider.GetRequiredService<IBookService>();
        }

        [Fact]
        public async void InsertBook_Test()
        {
            var faker = new Faker();

            var book = new BookInfoViewModel
            {
                Author = faker.Name.FullName(),
                Genre = faker.Random.Enum<BookGenre>(),
                NameBook = faker.Commerce.ProductName(),
                PublicationDate = DateOnly.FromDateTime(faker.Date.Past(20))
            };

            var result = await _bookService.InsertBookAsync(book);

            Assert.True(result > 0);
        }

        [Fact]
        public async void GetAllBooks_Test()
        {
            var result = await _bookService.GetAllBooksAsync();

            Assert.True(result.Any());
        }
    }
}