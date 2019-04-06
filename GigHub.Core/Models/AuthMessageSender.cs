using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GigHub.Core.Models
{
    public class AuthMessageSender: IEmailSender
    {

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com", // set your SMTP server name here
                Port = 587, // Port 
                EnableSsl = true,
                Credentials = new NetworkCredential("from@gmail.com", "password")
            };

            using (var message = new MailMessage(email, "to@mail.com")
            {
                Subject = subject,
                Body = htmlMessage
            })
            
            await smtpClient.SendMailAsync(message);
        }
    }
}
