using Desk.Application.Dtos;
using Desk.Application.UseCases.ViewTag;
using Desk.Application.UseCases.ViewTaggedUserItems;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Tags;

public class SearchModel : PageModel
{
    private readonly IMediator _mediator;

    public TagDto Tag { get; set; } = new();

    public List<SummaryItemDto> Items { get; set; } = [];

    public SearchModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int tagId, CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        var tagRequest = new ViewUserTagRequest(tagId, userId);
        var tagResponse = await _mediator.Send(tagRequest);

        if (tagResponse is null)
        {
            return NotFound();
        }

        Tag = tagResponse;

        var itemsRequest = new ViewTaggedUserItemsRequest(userId, tagId);
        var itemsResponse = await _mediator.Send(itemsRequest, ct);
        Items = itemsResponse;

        return Page();
    }
}
