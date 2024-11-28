using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.ViewUsersReport;

public class ViewUsersReportHandler : IRequestHandler<ViewUsersReportRequest, List<UserReportDto>>
{
    private readonly IReportRepository _reportRepository;

    private readonly ILogger<ViewUsersReportHandler> _logger;

    public ViewUsersReportHandler(IReportRepository reportRepository, ILogger<ViewUsersReportHandler> logger)
    {
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<UserReportDto>> Handle(ViewUsersReportRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("View users report - {@request}", request);
        var userReports = await _reportRepository.GetUserReportsAsync(cancellationToken);

        return userReports.Select(r => new UserReportDto
        {
            UserId = r.UserId,
            Username = r.Username,
            IsConfirmed = r.IsConfirmed,
            LastLoginOn = r.LastLoginOn
        }).ToList();
    }
}
