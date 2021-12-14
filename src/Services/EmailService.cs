using Services.Interfaces;
using Microsoft.Extensions.Options;
using Infrastructure.Options;
using System.Threading.Tasks;
using Infrastructure.Result.Interfaces;
using System.Net.Mail;
using System.Net;
using Infrastructure.Result;
using System;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSMTPOption _emailOption;

        public EmailService(
            IOptions<EmailSMTPOption> emailOptions)
        {
            _emailOption = emailOptions?.Value;
        }

        public async Task<IResult> SendEmailAsync(string recipient, string title, string body)
        {
            try
            {
                var from = new MailAddress(_emailOption.EmailAddress, _emailOption.DisplayName);
                MailAddress to = new MailAddress(recipient);
                MailMessage message = new MailMessage(from, to);
                message.Subject = title;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(_emailOption.EmailAddress, _emailOption.Password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);

                return Result.SuccessResult();
            }
            catch (Exception ex)
            {
                return Result.ErrorResult().BuildMessage(ex.Message);
            }
        }
    }
}
