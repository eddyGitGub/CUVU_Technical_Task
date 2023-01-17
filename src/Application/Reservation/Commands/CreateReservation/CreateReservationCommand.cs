using CUVU_Technical_Task.Application.Common.Interfaces;
using CUVU_Technical_Task.Application.Common.Models;
using CUVU_Technical_Task.Application.Extensions;
using CUVU_Technical_Task.Application.Reservation.Queries;
using CUVU_Technical_Task.Application.Reservation.Queries.GetBooking;
using CUVU_Technical_Task.Domain.Entities;
using CUVU_Technical_Task.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CUVU_Technical_Task.Application.Reservation.Commands.CreateReservation;

public record CreateReservationCommand : IRequest<Result>
{
    public string CustomerName { get; set; } = string.Empty;
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
}

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _sender;

    public CreateReservationCommandHandler(IApplicationDbContext context, ISender sender)
    {
        _context = context;
        _sender = sender;
    }

    public async Task<Result> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        if(request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        var isIsAvailable = await _sender.Send(new IsBookingSpaceAvailableQuery(request.From, request.To), cancellationToken);
        if (!isIsAvailable)
        {
            //throw new Exception("No spaces available for the given dates");
            return Result.Failure(new List<string>() { "No spaces available for the given dates" });
        }
       
        var entity = new Booking
        {
            CustomerName = request.CustomerName,
            DateFrom = request.From,
            DateTo = request.To,
            DurationInDay = request.From.ToDurationInDay(request.To),
        };

        entity.AddDomainEvent(new BookingCreatedEvent(entity));

        _context.Bookings.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
