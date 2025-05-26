using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;

namespace FluentEmail_ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // تنظیمات SMTP
            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 587,
                Credentials = new System.Net.NetworkCredential("abolfazlshs80@gmail.com", "ewnb cuaj cpim zgwy")
            });

            // مقداردهی اولیه FluentEmail
            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            // ارسال ایمیل
            var email = await Email
                .From("abolfazlshs80@gmail.com")
                .To("a95.shabani@gmail.com", "Recipient Name")
                .Subject(" hello from fluentsmtp")
                .Body("send message abolfazlshabani as messager")
                .SendAsync();

            Console.WriteLine(email.Successful
                ? "send message"
                : $"has error: {string.Join(", ", email.ErrorMessages)}");
        }
    }
}
