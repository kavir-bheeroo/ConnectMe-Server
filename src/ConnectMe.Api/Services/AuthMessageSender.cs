using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConnectMe.Api.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly IOptions<MailOptions> _mailOptions;

        public AuthMessageSender(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var emailTask = Task.Run(async () =>
            {
                try
                {
                    var emailMessage = new MimeMessage();

                    emailMessage.From.Add(new MailboxAddress("", _mailOptions.Value.MailFrom));
                    emailMessage.To.Add(new MailboxAddress("", email));
                    emailMessage.Subject = subject;
                    emailMessage.Body = new TextPart("plain") { Text = message };

                    using (var client = new SmtpClient())
                    {
                        //client.LocalDomain = "some.domain.com";
                        await client.ConnectAsync(_mailOptions.Value.SmtpServer, _mailOptions.Value.SmtpPort, SecureSocketOptions.None).ConfigureAwait(false);
                        await client.SendAsync(emailMessage).ConfigureAwait(false);
                        await client.DisconnectAsync(true).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return Task.FromResult(emailTask);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
