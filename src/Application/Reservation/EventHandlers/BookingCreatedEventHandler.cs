using CUVU_Technical_Task.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CUVU_Technical_Task.Application.Reservation.EventHandlers;

public class BookingCreatedEventHandler : INotificationHandler<BookingCreatedEvent>
{
    private readonly ILogger<BookingCreatedEventHandler> _logger;

    public BookingCreatedEventHandler(ILogger<BookingCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BookingCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CUVU_Technical_Task Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
