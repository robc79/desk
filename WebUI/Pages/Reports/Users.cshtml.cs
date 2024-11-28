using Desk.Application.Dtos;
using Desk.Application.UseCases.ViewUsersReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Reports;

public class UsersModel : PageModel
{
    private readonly IMediator _mediator;

    public List<UserReportDto> UserReports { get; set; } = [];

    public UsersModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var request = new ViewUsersReportRequest();
        var response = await _mediator.Send(request, ct);
        UserReports = response;
        
        return Page();
    }
}
