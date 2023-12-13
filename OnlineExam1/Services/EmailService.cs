using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using OnlineExam1.Models;

namespace OnlineExam1.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var emailConfig = _configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(emailConfig.From));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(emailConfig.SmtpServer, emailConfig.Port, true);
                client.Authenticate(emailConfig.SmtpUsername, emailConfig.SmtpPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}