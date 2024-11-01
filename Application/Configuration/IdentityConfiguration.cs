namespace Desk.Application.Configuration;

public class IdentityConfiguration
{
    public const string SectionName = "Identity";
    
    public bool RequireAuthenticatedEmail { get; set; }

    public bool RegistrationsEnabled { get; set; }
}