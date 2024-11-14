namespace Desk.Application.UseCases.DeleteUserItemImage;

public class DeleteUserItemImageResponse
{
    public string? Error { get; }

    public DeleteUserItemImageResponse(string? error)
    {
        Error = error;
    }
}