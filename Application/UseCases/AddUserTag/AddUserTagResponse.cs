namespace Desk.Application.UseCases.AddUserTag;

public class AddUserTagResponse
{
    public string? Error { get; }

    public int? TagId { get; }

    private AddUserTagResponse(int? tagId, string? error)
    {
        TagId = tagId;
        Error = error;
    }

    public static AddUserTagResponse Success(int id)
    {
        return new AddUserTagResponse(id, null);
    }

    public static AddUserTagResponse Failure(string error)
    {
        return new AddUserTagResponse(null, error);
    }
}