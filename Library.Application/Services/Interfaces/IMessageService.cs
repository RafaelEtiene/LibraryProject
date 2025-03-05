using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<bool> SendMessageAsync(string phoneNumberClient, string body);
    }
}
