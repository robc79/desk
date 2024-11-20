using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.ViewUsersReport;

public class ViewUsersReportHandler : IRequestHandler<ViewUsersReportRequest, List<UserReportDto>>
{
    private readonly IReportRepository _reportRepository;
    public ViewUsersReportHandler(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
    }

    public async Task<List<UserReportDto>> Handle(ViewUsersReportRequest request, CancellationToken cancellationToken)
    {
        var userReports = await _reportRepository.GetUserReportsAsync(cancellationToken);

        return userReports.Select(r => new UserReportDto
        {
            Username = r.Username,
            ItemCount = r.ItemCount,
            ImageCount = r.ImageCount
        }).ToList();
    }
}
