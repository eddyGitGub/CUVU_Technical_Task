using CUVU_Technical_Task.Application.Parking.Queries;
using CUVU_Technical_Task.Application.Reservation.Queries.GetAvailableParkingSpace;
using MediatR;
using Moq;
using Shouldly;

namespace Application.UnitTest.Reservation.Queries;
[Collection("QueryCollection")]
public class GetAvailableFreeSpaceQueryTests
{
    private readonly ApplicationDbContext _context;
    public GetAvailableFreeSpaceQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
    }
    [Fact]
    public async Task Handler_Should_Return_AvailableSapce()
    {
        //arrange
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        var mediatorMock = new Mock<ISender>();
        mediatorMock.Setup(x => x.Send(new GetParkingSpaceQuery(), CancellationToken.None))
          .ReturnsAsync(new CarParkVm { Capacity = 10 });
        var query = new GetAvailableFreeSpaceQuery(date, date.AddDays(1));
        var handler = new GetAvailableFreeSpaceQueryHandler(_context, mediatorMock.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<List<string>>();

        result.Count.ShouldBe(2);
    }
}
