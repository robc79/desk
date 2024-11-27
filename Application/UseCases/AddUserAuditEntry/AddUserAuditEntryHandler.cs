using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.AddUserAuditEntry;

public class AddUserAuditEntryHandler : IRequestHandler<AddUserAuditEntryRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserAuditEntryRepository _auditRepository;

    private readonly IUserRepository _userRepository;

    private readonly ILogger<AddUserAuditEntryHandler> _logger;

    public AddUserAuditEntryHandler(
        IUnitOfWork unitOfWork,
        IUserAuditEntryRepository auditRepository,
        IUserRepository userRepository,
        ILogger<AddUserAuditEntryHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(AddUserAuditEntryRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        var audit = new UserAuditEntry(user, request.EventType);
        await _auditRepository.AddAsync(audit, cancellationToken);

        try
        {
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to record audit for '{request.UserId}' - '{request.EventType}'.");
        }
    }
}
