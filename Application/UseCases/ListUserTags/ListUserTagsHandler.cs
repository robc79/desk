using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.ListUserTags;

public class ListUserTagsHandler : IRequestHandler<ListUserTagsRequest, List<TagDto>>
{
    private readonly ITagRepository _tagRepository;

    public ListUserTagsHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<List<TagDto>> Handle(ListUserTagsRequest request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetByUserAsync(request.UserId, cancellationToken);

        return tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList();
    }
}