using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ViewTag;

public class ViewTagRequest : IRequest<TagDto?>
{
    public int Id { get; }

    public ViewTagRequest(int id)
    {
        Id = id;
    }
}