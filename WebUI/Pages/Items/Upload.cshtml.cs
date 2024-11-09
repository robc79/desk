using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Desk.Application.Dtos;
using Desk.Application.UseCases.UpdateUserItemImage;
using Desk.Application.UseCases.ViewUserItem;
using Desk.Application.UseCases.ViewUserItemSummary;
using Desk.Shared;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Items;

[RequestFormLimits(MultipartBodyLengthLimit = Constants.MaxImageUploadSize)]
public class UploadModel : PageModel
{
    public class FormModel
    {
        [Required]
        [DisplayName("Image")]
        public IFormFile? ImageUpload { get; set; }
    }

    private readonly string[] _expectedImageExtensions = [ ".jpg", ".jpeg"];
    
    private readonly IMediator _mediator;

    [BindProperty]
    public FormModel Form { get; set; } = new();

    public SummaryItemDto SummaryItem { get; set; } = new();

    public UploadModel(IMediator mediator)
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        byte[] imageBytes;
        var untrustedExt = Path.GetExtension(Form.ImageUpload!.FileName.ToLowerInvariant());

        if (!_expectedImageExtensions.Contains(untrustedExt))
        {
            return BadRequest();
        }

        using (var ms = new MemoryStream())
        {
            await Form.ImageUpload.CopyToAsync(ms, ct);
            imageBytes = ms.ToArray();
        }
        
        var userId = HttpContext.UserIdentifier();
        var request = new UpdateUserItemImageRequest(userId, itemId, imageBytes);
        var response = await _mediator.Send(request, ct);

        if (response.Error is not null)
        {
            return StatusCode(500);
        }
        
        return RedirectToPage("/Items/View", new { itemId });
    }
}
