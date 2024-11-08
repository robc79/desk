namespace Desk.Application.Configuration;

public class WasabiConfiguration
{
    public const string SectionName = "Wasabi";
    
    public string Id { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;
}