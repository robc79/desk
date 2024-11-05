using Desk.Application.Dtos;
using Desk.Application.UseCases.ViewTag;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Tags;

public class SearchModel : PageModel
{
    private readonly IMediator _mediator;

    public TagDto Tag { get; set; } = new();

    public SearchModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int tagId, CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        var request = new ViewUserTagRequest(tagId, userId);
        var response = await _mediator.Send(request);

        if (response is null)
        {
            return NotFound();
        }

        Tag = response;

        return Page();
    }
}
