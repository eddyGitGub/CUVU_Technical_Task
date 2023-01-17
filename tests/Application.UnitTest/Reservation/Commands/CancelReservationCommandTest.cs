using CUVU_Technical_Task.Application.Common.Exceptions;
using CUVU_Technical_Task.Application.Reservation.Commands.CancelReservation;
using CUVU_Technical_Task.Domain.Entities;

namespace Application.UnitTest.Reservation.Commands;
public class CancelReservationCommandTest : CommandTestBase
{
    [Fact]
    public async Task Handler_Should_Return_Success_Result()
    {
        // Arrange
        var rId = CreateBooking();

        //var mediatorMock = new Mock<IMediator>();
        var request = new CancelReservationCommand(rId, "No Reason");
        var sut = new CancelReservationCommandHandler(_context);
        //Act
        var result = await sut.Handle(request, CancellationToken.None);
        var entity = await _context.Bookings
          .FindAsync(new object[] { request.Id }, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.True(entity?.IsCancel);
    }


    [Fact]
    public async Task Handler_Should_Throw_NotFoundException()
    {
        //Arrange
        var model = new CancelReservationCommand(1, "No Reason");
        var sut = new CancelReservationCommandHandler(_context);


        //Act
        var result = await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(model, CancellationToken.None));

        Assert.NotNull(result.Message);
    }

    private int CreateBooking()
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        _context.Bookings.Add(new Booking { CustomerName = "Edward", DateFrom = date, DateTo = date.AddDays(2) });
        var rId = _context.SaveChanges();
        return rId;

    }
}
