using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.AddUserItem;
using Desk.Application.UseCases.ListUserTags;
using Desk.Shared;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Desk.WebUI.Pages.Items;

[RequestFormLimits(MultipartBodyLengthLimit = Constants.MaxImageUploadSize)]
public class AddModel : PageModel
{
    public class FormModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public IFormFile? ImageUpload { get; set; }

        public string? Description { get; set; } = string.Empty;

        public int SelectedLocationId { get; set; }

        public int SelectedStatusId { get; set; }

        public int[]? SelectedTagIds { get; set; }
    }

    private readonly string[] _expectedImageExtensions = [ ".jpg", ".jpeg"];

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
        var userId = HttpContext.UserIdentifier();
        TagItems = await PopulateTagItemsAsync(userId, ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
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

        byte[]? imageBytes = null;

        if (Form.ImageUpload != null)
        {
            var untrustedExt = Path.GetExtension(Form.ImageUpload.FileName.ToLowerInvariant());

            if (!_expectedImageExtensions.Contains(untrustedExt))
            {
                return BadRequest();
            }

            using (var ms = new MemoryStream())
            {
                await Form.ImageUpload.CopyToAsync(ms, ct);
                imageBytes = ms.ToArray();
            }
        }
        
        var request = new AddUserItemRequest(
            userId,
            Form.Name,
            imageBytes,
            Form.Description,
            selectedLocation,
            selectedStatus,
            Form.SelectedTagIds
        );

        var response = await _mediator.Send(request);

        if (response.Error is not null)
        {
            return StatusCode(500);
        }

        return selectedLocation switch{
            ItemLocationEnum.Pile => RedirectToPage("/Items/Pile"),
            ItemLocationEnum.Desk => RedirectToPage("/Items/Desk"),
            ItemLocationEnum.Tabletop => RedirectToPage("/Items/Tabletop"),
            _ => RedirectToPage("/Items/Pile")
        };
    }

    private async Task<SelectList> PopulateTagItemsAsync(Guid userId, CancellationToken ct)
    {
        var request = new ListUserTagsRequest(userId);
        var response = await _mediator.Send(request, ct);
        var list = new SelectList(response, nameof(TagDto.Id), nameof(TagDto.Name));

        return list;
    }
}