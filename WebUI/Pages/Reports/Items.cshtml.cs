using Desk.Application.Dtos;
using Desk.Application.UseCases.ViewItemsReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Reports;

public class ItemsModel : PageModel
{
    private readonly IMediator _mediator;

    public List<ItemReportDto> ItemReports { get; set; } = [];

    public ItemsModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var request = new ViewItemsReportRequest();
        var response = await _mediator.Send(request, ct);
        ItemReports = response;
        
        return Page();
    }
}
