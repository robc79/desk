using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.ViewTag;

public class ViewTagHandler : IRequestHandler<ViewTagRequest, TagDto?>
{
    private readonly ITagRepository _tagRepository;

    public ViewTagHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));        
    }

    public Task<TagDto?> Handle(ViewTagRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
