namespace Desk.Application.UseCases.UpdateUserItem;

public class UpdateUserItemResponse
{
    public string? Error { get; }

    public UpdateUserItemResponse(string? error)
    {
        Error = error;
    }
}