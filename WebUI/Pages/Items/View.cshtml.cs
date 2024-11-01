using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.ViewUserItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Items;

public class ViewModel : PageModel
{
    public class EditDescriptionFormModel
    {
        public string? Description { get; set; } = string.Empty;
    }

    private readonly IMediator _mediator;

    public FullItemDto? Item { get; set; }

    public bool IsEditable { get; set; } = false;

    [BindProperty]
    public EditDescriptionFormModel EditDescriptionForm { get; set; } = new EditDescriptionFormModel();

    public ViewModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int itemId, CancellationToken ct)
    {
        var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(idClaim!.Value);
        var request = new ViewUserItemRequest(userId, itemId);
        var response = await _mediator.Send(request, ct);

        if (response is null)
        {
            return NotFound();
        }
        
        Item = response;

        if (Request.Query.ContainsKey("isEditable"))
        {
            IsEditable = true;
        }


        return Page();
    }

    public async Task<IActionResult> OnPostEditDescriptionAsync(int itemId, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Call out to mediator, update the item description.

        return RedirectToPage("/Items/View");
    }
}
