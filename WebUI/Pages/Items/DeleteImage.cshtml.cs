using Desk.Application.Dtos;
using Desk.Application.UseCases.DeleteUserItemImage;
using Desk.Application.UseCases.ViewUserItemSummary;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Items;

public class DeleteImageModel : PageModel
{
    private readonly IMediator _mediator;

    public SummaryItemDto? SummaryItem { get; set; }

    public DeleteImageModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int itemId, CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        var request = new ViewUserItemSummaryRequest(userId, itemId);
        var response = await _mediator.Send(request, ct);

        if (response is null)
        {
            return NotFound();
        }

        SummaryItem = response;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int itemId, CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        var request = new DeleteUserItemImageRequest(userId, itemId);
        var response = await _mediator.Send(request);

        if (response.Error is not null)
        {
            return StatusCode(500);
        }

        return RedirectToPage("/Items/View", new { itemId });
    }
}
