using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.ListUserTags;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Tags;

[Authorize]
public class ListModel : PageModel
{
    private readonly IMediator _mediator;

    public ListModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); 
    }

    public List<TagDto> Tags { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(idClaim!.Value);
        var request = new ListUserTagsRequest(userId);
        var response = await _mediator.Send(request, ct);
        Tags = response;

        return Page();
    }
}
