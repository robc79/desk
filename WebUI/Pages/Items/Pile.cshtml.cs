using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.ViewUserItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Items;

public class PileModel : PageModel
{
    private readonly IMediator _mediator;

    public PileModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public List<SummaryItemDto> PileItems { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(idClaim!.Value);
        var request = new ViewUserItemsRequest(userId, ItemLocationEnum.Pile);
        var response = await _mediator.Send(request, ct);
        PileItems = response;
    }
}