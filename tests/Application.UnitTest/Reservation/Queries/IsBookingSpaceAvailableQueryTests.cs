using CUVU_Technical_Task.Application.Parking.Queries;
using CUVU_Technical_Task.Application.Reservation.Queries;
using CUVU_Technical_Task.Domain.Entities;
using MediatR;
using Moq;
using Shouldly;

namespace Application.UnitTest.Reservation.Queries;
[Collection("QueryCollection")]
public class IsBookingSpaceAvailableQueryTests
{
    private readonly ApplicationDbContext _context;
    public IsBookingSpaceAvailableQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
    }
    [Fact]
    public async Task Handler_Should_Return_True()
    {
        //arrange
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        var mediatorMock = new Mock<ISender>();
        mediatorMock.Setup(x => x.Send(new GetParkingSpaceQuery(), CancellationToken.None))
          .ReturnsAsync(new CarParkVm { Capacity = 10 });
        var query = new IsBookingSpaceAvailableQuery(date, date.AddDays(1));
        var handler = new IsBookingSpaceAvailableQueryHandler(_context, mediatorMock.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.ShouldBe(true);
    }

    [Fact]
    public async Task Handler_Should_Return_False()
    {
        //arrange
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        var mediatorMock = new Mock<ISender>();
        mediatorMock.Setup(x => x.Send(new GetParkingSpaceQuery(), CancellationToken.None))
          .ReturnsAsync(new CarParkVm { Capacity = 2 });
        var newBookings = new List<Booking>{
            new Booking { CustomerName = "First Name", DateFrom = date, DateTo = date.AddDays(1) },
            new Booking { CustomerName = "First Name", DateFrom = date, DateTo = date.AddDays(1) }
        };
        await _context.Bookings.AddRangeAsync(newBookings);
        await _context.SaveChangesAsync();
        var query = new IsBookingSpaceAvailableQuery(date, date.AddDays(1));
        var handler = new IsBookingSpaceAvailableQueryHandler(_context, mediatorMock.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.ShouldBe(false);
    }
}
