using Desk.Application.Dtos;
using Desk.Application.UseCases.DeleteUserItem;
using Desk.Application.UseCases.ViewUserItemSummary;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Items;

public class DeleteModel : PageModel
{
    private readonly IMediator _mediator;

    public SummaryItemDto? Item { get; set; }

    public DeleteModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int itemId, CancellationToken ct)
    {
        Item = await FetchItem(itemId, ct);

        if (Item is null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int itemId, CancellationToken ct)
    {
        Item = await FetchItem(itemId, ct);

        if (Item is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new DeleteUserItemRequest(HttpContext.UserIdentifier(), itemId);
        var response = await _mediator.Send(request, ct);
        
        if (response.Error is not null)
        {
            return StatusCode(500);
        }

        var redirectPage = Item!.Location switch
        {
            ItemLocationEnum.Pile => "/Items/Pile",
            ItemLocationEnum.Desk => "/Items/Desk",
            ItemLocationEnum.Tabletop => "/Items/Tabletop",
            _ => "/"
        };

        return RedirectToPage(redirectPage);
    }

    private async Task<SummaryItemDto?> FetchItem(int itemId, CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        var request = new ViewUserItemSummaryRequest(userId, itemId);
        var response = await _mediator.Send(request, ct);

        return response;
    }
}
