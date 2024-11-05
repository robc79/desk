using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.AddUserTag;
using Desk.Application.UseCases.ListUserTags;
using Desk.Shared;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Tags;

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
        var userId = HttpContext.UserIdentifier();
        var request = new ListUserTagsRequest(userId);
        var response = await _mediator.Send(request, ct);
        Tags = response;

        return Page();
    }
}
