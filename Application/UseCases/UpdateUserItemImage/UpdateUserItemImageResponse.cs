namespace Desk.Application.UseCases.UpdateUserItemImage;

public class UpdateUserItemImageResponse
{
    public string? Error { get; }

    public UpdateUserItemImageResponse(string? error)
    {
        Error = error;
    }
}