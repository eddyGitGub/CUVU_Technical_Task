using CUVU_Technical_Task.Application.Common.Exceptions;
using CUVU_Technical_Task.Application.Common.Interfaces;
using CUVU_Technical_Task.Application.Common.Models;
using CUVU_Technical_Task.Application.Extensions;
using CUVU_Technical_Task.Application.Reservation.Queries;
using CUVU_Technical_Task.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUVU_Technical_Task.Application.Reservation.Commands.UpdateReservation;

public record UpdateReservationCommand : IRequest<Result>
{
    public int Id { get; init; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
}

public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _sender;

    public UpdateReservationCommandHandler(IApplicationDbContext context, ISender sender)
    {
        _context = context;
        _sender = sender;
    }

    public async Task<Result> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bookings
            .FirstOrDefaultAsync(x=>x.Id == request.Id && !x.IsCancel, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Booking), request.Id);
        }
        var isIsAvailable = await _sender.Send(new IsBookingSpaceAvailableQuery(request.From, request.To), cancellationToken);
        if (!isIsAvailable)
        {
            //throw new Exception("No spaces available for the given dates");
            return Result.Failure(new List<string>() { "No spaces available for the given dates" });
        }
        entity.DateFrom = request.From;
        entity.DateTo = request.To;
        entity.DurationInDay = request.From.ToDurationInDay(request.To);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
