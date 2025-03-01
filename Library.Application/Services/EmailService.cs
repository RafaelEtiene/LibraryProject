using Library.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Library.Application.Services
{

    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _smtpUsername = configuration["EmailSettings:SmtpUsername"];
            _smtpPassword = configuration["EmailSettings:SmtpPassword"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = false)
        {
            string apiKey = "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";
            string fromEmail = "rafaellimaetiene10@gmail.com";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(
                    $@"{{
                    'personalizations': [{{'to': [{{'email': '{toEmail}'}}]}}],
                    'from': {{'email': '{fromEmail}'}},
                    'subject': 'Assunto do E-mail',
                    'content': [{{'type': 'text/plain', 'value': 'Corpo do e-mail.'}}]
                }}",
                    Encoding.UTF8,
                    "application/json"
                );

                HttpResponseMessage response = await client.PostAsync("https://api.sendgrid.com/v3/mail/send", content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("E-mail enviado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro ao enviar e-mail: " + await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
