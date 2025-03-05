using Library.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Library.Application.Services
{

    public class MessageService : IMessageService
    {
        private readonly string _phoneNumberTwilio;
        private readonly string _accountSid;
        private readonly string _authToken;

        public MessageService(IConfiguration configuration)
        {
            _phoneNumberTwilio = configuration["SmsSettings:PhoneTwilio"];
            _accountSid = configuration["SmsSettings:AccountSID"];
            _authToken = configuration["SmsSettings:AuthToken"];
        }

        public async Task<bool> SendMessageAsync(string phoneNumberClient, string body)
        {
            try
            {
                string accountSid = _accountSid;
                string authToken = _authToken;

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: body,
                    from: new PhoneNumber(_phoneNumberTwilio),
                    to: new PhoneNumber($"+55{phoneNumberClient}")
                );

                Console.WriteLine("Message sended with success! SID: " + message.Sid);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error during sending message. Error: {ex.Message}");
                return false;
            }    
        }
    }
}
