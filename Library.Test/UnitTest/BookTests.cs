using Bogus;
using Library.Application.Services;
using Library.Application.Services.Interfaces;
using Library.Application.ViewModel;
using Library.Core.Entities;
using Library.Core.Enum;
using Library.Infrastructure.Data.Repositories;
using Library.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Library.Test.UnitTest
{
    public class BookTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IBookService _bookService;

        public BookTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IBookRepository, BookRepository>();
            serviceCollection.AddScoped<IBookService, BookService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _bookService = _serviceProvider.GetService<BookService>();

        }
        [Fact]
        public async void InsertBook_Test()
        {
            var faker = new Faker();

            var book = new BookInfoViewModel
            {
                Author = faker.Name.FullName(),  // Nome do autor aleatório
                Genre = faker.Random.Enum<BookGenre>(),  // Gênero aleatório
                NameBook = faker.Commerce.ProductName(),  // Nome do livro aleatório
                PublicationDate = faker.Date.Past(20)  // Data de publicação aleatória (nos últimos 20 anos)
            };

            var result = await _bookService.InsertBookAsync(book);

            Assert.True(result > 0);
        }
    }
}