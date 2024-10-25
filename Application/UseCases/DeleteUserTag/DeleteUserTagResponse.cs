namespace Desk.Application.UseCases.DeleteUserTag;

public class DeleteUserTagResponse
{
    public string? Error { get; }

    public DeleteUserTagResponse(string? error = null)
    {
        Error = error;
    }
}