using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.ListUserTags;
using Desk.Application.UseCases.UpdateUserItem;
using Desk.Application.UseCases.ViewUserItem;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Desk.WebUI.Pages.Items;

public class EditModel : PageModel
{
    public class FormModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public int SelectedLocationId { get; set; }

        public int SelectedStatusId { get; set; }

        public int[] SelectedTagIds { get; set; } = [];
    }

    private readonly IMediator _mediator;

    [BindProperty]
    public FormModel Form { get; set; } = new FormModel();

    public SelectList? TagItems { get; set; }

    public EditModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int itemId, CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        TagItems = await PopulateTagItemsAsync(userId, ct);
        
        var request = new ViewUserItemRequest(userId, itemId);
        var response = await _mediator.Send(request, ct);

        if (response is null)
        {
            return NotFound();
        }

        Form.Name = response.Name!;
        Form.Description = response.Description;
        Form.SelectedLocationId = (int)response.Location;
        Form.SelectedStatusId = (int)response.CurrentStatus;
        Form.SelectedTagIds = response.Tags.Select(t => t.Id).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int itemId, CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        await PopulateTagItemsAsync(userId, ct);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new UpdateUserItemRequest(
            userId,
            itemId,
            Form.Name,
            Form.Description,
            (ItemStatusEnum)Form.SelectedStatusId,
            (ItemLocationEnum)Form.SelectedLocationId,
            Form.SelectedTagIds);

        var response = await _mediator.Send(request, ct);

        if (response.Error is not null)
        {
            return StatusCode(500);
        }

        return RedirectToPage("/Items/View", new { itemId });
    }

    private async Task<SelectList> PopulateTagItemsAsync(Guid userId, CancellationToken ct)
    {
        var request = new ListUserTagsRequest(userId);
        var response = await _mediator.Send(request, ct);
        var list = new SelectList(response, nameof(TagDto.Id), nameof(TagDto.Name));

        return list;
    }
}
