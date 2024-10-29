using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.AddUserItem;
using Desk.Application.UseCases.ListUserTags;
using Desk.Application.UseCases.ViewTag;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Desk.WebUI.Pages.Items;

public class AddModel : PageModel
{
    public class FormModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public int SelectedLocationId { get; set; }

        public int SelectedStatusId { get; set; }

        public int[]? SelectedTagIds { get; set; }
    }

    private readonly IMediator _mediator;

    [BindProperty]
    public FormModel Form { get; set; } = new FormModel();

    public SelectList? TagItems { get; set; }

    public AddModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task OnGetAsync(CancellationToken ct)
    {
        var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(idClaim!.Value);
        TagItems = await PopulateTagItemsAsync(userId, ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(idClaim!.Value);
        TagItems = await PopulateTagItemsAsync(userId, ct);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var selectedLocation = (ItemLocationEnum)Form.SelectedLocationId;
        
        if (!Enum.IsDefined(selectedLocation))
        {
            return BadRequest();
        }

        var selectedStatus = (ItemStatusEnum)Form.SelectedStatusId;

        if (!Enum.IsDefined(selectedStatus))
        {
            return BadRequest();
        }
        
        var request = new AddUserItemRequest(
            userId,
            Form.Name,
            Form.Description,
            selectedLocation,
            selectedStatus,
            Form.SelectedTagIds
        );

        var response = await _mediator.Send(request);

        if (response.Error is not null)
        {
            // TODO: Indicate there is an error.
            return Page();
        }

        return RedirectToPage("/Items/Desk");
    }

    private async Task<SelectList> PopulateTagItemsAsync(Guid userId, CancellationToken ct)
    {
        var request = new ListUserTagsRequest(userId);
        var response = await _mediator.Send(request, ct);
        var list = new SelectList(response, nameof(TagDto.Id), nameof(TagDto.Name));

        return list;
    }
}