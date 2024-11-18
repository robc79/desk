using Desk.Application.Dtos;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.ViewUsersReport;

public class ViewUsersReportHandler : IRequestHandler<ViewUsersReportRequest, List<UserReportDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IItemRepository _itemRepository;

    public ViewUsersReportHandler(
        IUserRepository userRepository,
        IItemRepository itemRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<List<UserReportDto>> Handle(ViewUsersReportRequest request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        var dtos = new List<UserReportDto>();

        foreach (var user in users)
        {
            var items = await _itemRepository.CountUserItemsAsync(user.Id, false, cancellationToken);
            var images = await _itemRepository.CountUserItemsAsync(user.Id, true, cancellationToken);
            dtos.Add(new UserReportDto { Id = user.Id, Username = user.UserName, ItemCount = items, ImageCount = images});
        }
        
        return dtos;
    }
}
