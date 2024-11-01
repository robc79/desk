using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.ViewUserItem;
using Desk.WebUI.Extensions;
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

    [BindProperty]
    public EditDescriptionFormModel EditDescriptionForm { get; set; } = new EditDescriptionFormModel();

    public ViewModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int itemId, CancellationToken ct)
    {
        var userId = HttpContext.User.UserIdentifier(HttpContext);
        var request = new ViewUserItemRequest(userId, itemId);
        var response = await _mediator.Send(request, ct);

        if (response is null)
        {
            return NotFound();
        }
        
        Item = response;
        
        return Page();
    }
}
