using CUVU_Technical_Task.Application.Common.Exceptions;
using CUVU_Technical_Task.Application.Common.Interfaces;
using CUVU_Technical_Task.Application.Common.Models;
using CUVU_Technical_Task.Domain.Entities;
using MediatR;

namespace CUVU_Technical_Task.Application.Reservation.Commands.CancelReservation;

public record CancelReservationCommand(int Id, string CancelReason) : IRequest<Result>;
public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CancelReservationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bookings
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Booking), request.Id);
        }
        entity.IsCancel = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
