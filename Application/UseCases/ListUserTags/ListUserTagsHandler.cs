using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.ListUserTags;

public class ListUserTagsHandler : IRequestHandler<ListUserTagsRequest, List<TagDto>>
{
    private readonly ITagRepository _tagRepository;

    private readonly ILogger<ListUserTagsHandler> _logger;

    public ListUserTagsHandler(ITagRepository tagRepository, ILogger<ListUserTagsHandler> logger)
    {
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<TagDto>> Handle(ListUserTagsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("List user tags - {@request}", request);
        var tags = await _tagRepository.GetByUserAsync(request.UserId, cancellationToken);

        return tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList();
    }
}