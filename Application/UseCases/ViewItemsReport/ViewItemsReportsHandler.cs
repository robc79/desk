using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.ViewItemsReport;

public class ViewItemsReportHandler : IRequestHandler<ViewItemsReportRequest, List<ItemReportDto>>
{
    private readonly IReportRepository _reportRepository;

    private readonly ILogger<ViewItemsReportHandler> _logger;

    public ViewItemsReportHandler(IReportRepository reportRepository, ILogger<ViewItemsReportHandler> logger)
    {
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<ItemReportDto>> Handle(ViewItemsReportRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("View items report - {@request}", request);
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
