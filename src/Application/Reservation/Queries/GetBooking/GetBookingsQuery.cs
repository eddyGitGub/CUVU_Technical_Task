using AutoMapper;
using AutoMapper.QueryableExtensions;
using CUVU_Technical_Task.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUVU_Technical_Task.Application.Reservation.Queries.GetBooking;
public record GetBookingsQuery : IRequest<List<BookingVm>>;

public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, List<BookingVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBookingsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BookingVm>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        var lists = await _context.Bookings
                  .AsNoTracking()
                  .ProjectTo<BookingVm>(_mapper.ConfigurationProvider)
                  .OrderBy(t => t.CustomerName)
                  .ToListAsync(cancellationToken);

        return lists;
    }
}

