using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.ViewTag;

public class ViewUserTagHandler : IRequestHandler<ViewUserTagRequest, TagDto?>
{
    private readonly ITagRepository _tagRepository;

    public ViewUserTagHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));        
    }

    public async Task<TagDto?> Handle(ViewUserTagRequest request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetByUserAndIdAsync(request.TagId, request.UserId, cancellationToken);

        return tag is null
        ? null
        : new TagDto
        {
            Id = tag.Id, 
            Name = tag.Name,
            Owner = new UserDto
            {
                Id = tag.Owner.Id,
                Username = tag.Owner.Username
            }
        };
    }
}
