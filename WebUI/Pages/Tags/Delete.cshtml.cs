using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.DeleteUserTag;
using Desk.Application.UseCases.ViewTag;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Tags
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public TagDto? Tag { get; set; }

        public async Task<IActionResult> OnGetAsync(int tagId, CancellationToken ct)
        {
            var userId = HttpContext.UserIdentifier();
            var request = new ViewUserTagRequest(tagId, userId);
            var response = await _mediator.Send(request, ct);
            
            if (response is null)
            {
                return RedirectToPage("/Tags/List");
            }

            Tag = response;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int tagId, CancellationToken ct)
        {
            var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(idClaim!.Value);
            var request = new DeleteUserTagRequest(tagId, userId);
            var response = await _mediator.Send(request, ct);

            if (response.Error is not null)
            {
                return Page();
            }
            
            return RedirectToPage("/Tags/List");
        }
    }
}
