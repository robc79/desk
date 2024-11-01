namespace Desk.Application.UseCases.UpdateUserItemDescription;

public class UpdateUserItemDescriptionResponse
{
    public string? Error { get; }

    public UpdateUserItemDescriptionResponse(string? error)
    {
        Error = error;
    }
}