namespace Desk.Application.Configuration;

public class EmailSenderConfiguration
{
    public const string SectionName = "EmailSender";

    public string SmtpHost { get; set; } = string.Empty;

    public string SmtpUser { get; set; } = string.Empty;

    public string SmtpPassword { get; set; } = string.Empty;

    public int SmtpPort { get; set; }
}