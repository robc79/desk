using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ViewItemsReport;

public class ViewItemsReportRequest : IRequest<List<ItemReportDto>>
{
}