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
    public class SendMessageTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IMessageService _messageService;

        public SendMessageTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddInfrastructureServices();
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _messageService = _serviceProvider.GetRequiredService<IMessageService>();
        }

        [Fact]
        public async void SendMessage_Test()
        {
            var phoneNumberClient = "41997849659";
            var body = "Olá! Isso é apenas um teste";

            var result = await _messageService.SendMessageAsync(phoneNumberClient, body);
            Assert.True(result);
        }
    }
}