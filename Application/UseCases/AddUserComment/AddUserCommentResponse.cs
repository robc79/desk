namespace Desk.Application.UseCases.AddUserComment;

public class AddUserCommentResponse
{
    public int? Id { get; }

    public string? Error { get; }

    private AddUserCommentResponse(int? id, string? error)
    {
        Id = id;
        Error = error;
    }

    public static AddUserCommentResponse Success(int id) => new(id, null);

    public static AddUserCommentResponse Failure(string error) => new(null, error);
}