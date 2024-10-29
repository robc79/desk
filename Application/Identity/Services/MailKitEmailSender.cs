using Desk.Application.Configuration;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Desk.Application.Identity.Services;

public class MailKitEmailSender : IEmailSender
{
    private readonly EmailSenderConfiguration _config;

    public MailKitEmailSender(EmailSenderConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Painting Desk", _config.SmtpUser));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = htmlMessage;

        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect(_config.SmtpHost, _config.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);
            client.Authenticate(_config.SmtpUser, _config.SmtpPassword);
            client.Send(message);
            client.Disconnect(true);
        }

        return Task.CompletedTask;
    }
}
