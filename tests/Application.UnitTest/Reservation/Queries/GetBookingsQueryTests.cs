using CUVU_Technical_Task.Application.Reservation.Queries.GetBooking;
using Shouldly;

namespace Application.UnitTest.Reservation.Queries;
[Collection("QueryCollection")]
public class GetBookingsQueryTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetBookingsQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }
    [Fact]
    public async Task Handler_Should_Return_BookingReservation()
    {

        var query = new GetBookingsQuery();
        var handler = new GetBookingsQueryHandler(_context, _mapper);
        var result = await handler.Handle(query, CancellationToken.None);

        result.ShouldBeOfType<List<BookingVm>>();
    }
}
