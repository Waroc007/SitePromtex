using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Services
{
    public class EmailYandexWorker : IEmailWorker
    {
        private readonly string Email;
        private readonly string Password;
        public EmailYandexWorker(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public async Task SendEmail(string text, string emailSentTo)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Заявка с сайта", Email));
                message.To.Add(new MailboxAddress("Менеджер", emailSentTo));
                message.Subject = "Заявка с сайта";

                var builder = new BodyBuilder();

                    builder.TextBody = text +
                         "\n Письмо отправленно автоматически";

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    //client.CheckCertificateRevocation = true;
                    await client.ConnectAsync("smtp.yandex.ru", 465, true);
                    await client.AuthenticateAsync(Email, Password);
                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task SendEmail(string tel, string email, string comment, string emailSentTo)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Заявка с сайта", Email));
                message.To.Add(new MailboxAddress("Менеджер", emailSentTo));
                message.Subject = "Заявка с сайта";

                var builder = new BodyBuilder();

                if (comment != null)
                {
                    builder.TextBody = "Номер телефона: " + tel + "\n" +
                        "Email: " + email + "\n" +
                        "Комментарий: " + comment + "\n" +
                         "\n Письмо отправленно автоматически";
                }
                else
                {
                    builder.TextBody = "Номер телефона: " + tel + "\n" +
                        "Email: " + email + "\n" +
                         "\n Письмо отправленно автоматически";
                }

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    //client.CheckCertificateRevocation = true;
                    await client.ConnectAsync("smtp.yandex.ru", 465, true);
                    await client.AuthenticateAsync(Email, Password);
                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
