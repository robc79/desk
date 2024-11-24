using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.ViewItemsReport;

public class ViewItemsReportHandler : IRequestHandler<ViewItemsReportRequest, List<ItemReportDto>>
{
    private readonly IReportRepository _reportRepository;

    public ViewItemsReportHandler(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
    }

    public async Task<List<ItemReportDto>> Handle(ViewItemsReportRequest request, CancellationToken cancellationToken)
    {
        var itemReports = await _reportRepository.GetItemReportsAsync(cancellationToken);

        return itemReports.Select(r => new ItemReportDto
        {
            Username = r.Username,
            ItemCount = r.ItemCount,
            DeletedCount = r.DeletedCount,
            ImageCount = r.ImageCount
        }).ToList();
    }
}
