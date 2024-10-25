using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.AddUserTag;
using Desk.Application.UseCases.ListUserTags;
using Desk.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Tags;

[Authorize]
public class ListModel : PageModel
{
    public class FormModel
    {
        [Required]
        [MaxLength(Constants.MaxTagLength)]
        public string Name { get; set; } = string.Empty;
    }
    
    private readonly IMediator _mediator;

    public ListModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); 
    }

    [BindProperty]
    public FormModel Form { get; set; } = new FormModel();

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

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(idClaim!.Value);
        var request = new AddUserTagRequest(userId, Form.Name);
        var response = await _mediator.Send(request);
        // TODO: Handle error in a visual way.

        return RedirectToPage("/Tags/List");
    }
}
