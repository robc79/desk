using Microsoft.AspNetCore.Identity.UI.Services;

namespace Desk.Application.Identity;

public class DummyEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}