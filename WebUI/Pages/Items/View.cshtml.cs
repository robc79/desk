using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.AddUserComment;
using Desk.Application.UseCases.ViewUserItem;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Desk.WebUI.Pages.Items;

public class ViewModel : PageModel
{
    public class FormModel
    {
        [Required]
        public string Comment { get; set; } = string.Empty;
    }

    private readonly IMediator _mediator;

    public FullItemDto? Item { get; set; }

    public string? Base64EncodedItemImage { get; set; }

    [BindProperty]
    public FormModel Form { get; set; } = new FormModel();

    public ViewModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int itemId, CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        var request = new ViewUserItemRequest(userId, itemId);
        var response = await _mediator.Send(request, ct);

        if (response is null)
        {
            return NotFound();
        }
        
        Item = response;

        if (Item.ImageBytes is not null)
        {
            Base64EncodedItemImage = Convert.ToBase64String(Item.ImageBytes);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int itemId, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var userId = HttpContext.UserIdentifier();
        var request = new AddUserCommentRequest(userId, itemId, Form.Comment);
        var response = await _mediator.Send(request, ct);

        if (response.Error is not null)
        {
            return StatusCode(500);
        }

        return RedirectToPage("/Items/View", new { itemId });
    }
}
