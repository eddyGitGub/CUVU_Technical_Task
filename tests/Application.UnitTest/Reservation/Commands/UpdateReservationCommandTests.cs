using CUVU_Technical_Task.Application.Common.Exceptions;
using CUVU_Technical_Task.Application.Reservation.Commands.UpdateReservation;
using CUVU_Technical_Task.Application.Reservation.Queries;
using CUVU_Technical_Task.Domain.Entities;
using MediatR;
using Moq;

namespace Application.UnitTest.Reservation.Commands;
public class UpdateReservationCommandTests : CommandTestBase
{
    [Fact]
    public async Task Handler_Should_Return_Success_Result()
    {
        // Arrange
        var rId = CreateBooking();
        var date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3));
        var mediatorMock = new Mock<ISender>();
        var request = new UpdateReservationCommand
        {
            Id = rId,
            From = date,
            To = date.AddDays(1)
        };
        mediatorMock.Setup(x => x.Send(new IsBookingSpaceAvailableQuery(request.From, request.To), CancellationToken.None))
          .ReturnsAsync(true);
        var sut = new UpdateReservationCommandHandler(_context, mediatorMock.Object);
        //Act
        var result = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);

    }


    [Fact]
    public async Task Handler_Should_Throw_NotFoundException()
    {
        //Arrange
        var model = new UpdateReservationCommand { Id = 1 };
        var mediatorMock = new Mock<ISender>();
        //mediatorMock.Setup(x => x.Send(new IsBookingSpaceAvailableQuery(request.From, request.To), CancellationToken.None))
        //   .ReturnsAsync(true);
        var sut = new UpdateReservationCommandHandler(_context, mediatorMock.Object);


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
