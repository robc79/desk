using System.Security.Claims;
using Desk.Application.Dtos;
using Desk.Application.UseCases.ViewUserItems;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Items;

public class DeskModel : PageModel
{
    private readonly IMediator _mediator;

    public DeskModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public List<SummaryItemDto> DeskItems { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var userId = HttpContext.UserIdentifier();
        var request = new ViewUserItemsRequest(userId, ItemLocationEnum.Desk);
        var response = await _mediator.Send(request, ct);
        DeskItems = response;
    }
}