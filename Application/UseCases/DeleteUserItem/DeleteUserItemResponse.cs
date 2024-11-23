namespace Desk.Application.UseCases.DeleteUserItem;

public class DeleteUserItemResponse
{
    public string? Error { get; }

    public DeleteUserItemResponse(string? error)
    {
        Error = error;
    }
}