using MediatR;

namespace Desk.Application.UseCases.AddUserTag;

public class AddUserTagRequest : IRequest<AddUserTagResponse>
{
    public Guid UserId { get; }

    public string Name { get; }

    public AddUserTagRequest(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}