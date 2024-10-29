namespace Desk.Application.UseCases.AddUserItem;

public class AddUserItemResponse
{
        public string? Error { get; }

    public int? ItemId { get; }

    private AddUserItemResponse(int? itemId, string? error)
    {
        ItemId = itemId;
        Error = error;
    }

    public static AddUserItemResponse Success(int id)
    {
        return new AddUserItemResponse(id, null);
    }

    public static AddUserItemResponse Failure(string error)
    {
        return new AddUserItemResponse(null, error);
    }
}