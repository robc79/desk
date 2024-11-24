using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.ViewTag;

public class ViewUserTagHandler : IRequestHandler<ViewUserTagRequest, TagDto?>
{
    private readonly ITagRepository _tagRepository;

    private readonly ILogger<ViewUserTagHandler> _logger;

    public ViewUserTagHandler(ITagRepository tagRepository, ILogger<ViewUserTagHandler> logger)
    {
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));        
    }

    public async Task<TagDto?> Handle(ViewUserTagRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("View user tag - {@request}", request);
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
                Username = tag.Owner.UserName
            }
        };
    }
}
