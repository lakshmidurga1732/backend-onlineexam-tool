using OnlineExam1.DTO;
using OnlineExam1.Models;

namespace OnlineExam1.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }

}