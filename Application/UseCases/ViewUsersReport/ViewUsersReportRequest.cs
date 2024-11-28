using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ViewUsersReport;

public class ViewUsersReportRequest : IRequest<List<UserReportDto>>
{
}