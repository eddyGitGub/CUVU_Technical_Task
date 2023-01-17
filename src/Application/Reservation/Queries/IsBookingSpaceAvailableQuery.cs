using CUVU_Technical_Task.Application.Common.Interfaces;
using CUVU_Technical_Task.Application.Parking.Queries;
using MediatR;

namespace CUVU_Technical_Task.Application.Reservation.Queries;
public record IsBookingSpaceAvailableQuery(DateOnly DateFrom, DateOnly DateTo) : IRequest<bool>;
public class IsBookingSpaceAvailableQueryHandler : IRequestHandler<IsBookingSpaceAvailableQuery, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _sender;

    public IsBookingSpaceAvailableQueryHandler(IApplicationDbContext context, ISender sender)
    {
        _context = context;
        _sender = sender;
    }

    public async Task<bool> Handle(IsBookingSpaceAvailableQuery request, CancellationToken cancellationToken)
    {
        var parkingSpace = await _sender.Send(new GetParkingSpaceQuery(), cancellationToken);
        var totalSpace = parkingSpace?.Capacity ?? 10;

        var dateFrom = request.DateFrom;
        var dateTo = request.DateTo;
        int bookedSpaces = _context.Bookings.Where(b => (b.DateFrom <= dateFrom && b.DateTo >= dateFrom && !b.IsCancel) ||
        (b.DateFrom <= dateTo && b.DateTo >= dateTo && !b.IsCancel) ||
                                                (b.DateFrom >= dateFrom && b.DateTo <= dateTo && !b.IsCancel)).Count();

        return await Task.FromResult(bookedSpaces < totalSpace);
    }
}