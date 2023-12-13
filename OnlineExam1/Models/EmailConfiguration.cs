using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
namespace OnlineExam1.Models
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }




    }
}
