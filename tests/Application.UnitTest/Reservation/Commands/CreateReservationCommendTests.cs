using CUVU_Technical_Task.Application.Reservation.Commands.CreateReservation;
using CUVU_Technical_Task.Application.Reservation.Queries;
using MediatR;
using Moq;

namespace Application.UnitTest.Reservation.Commands;
public class CreateReservationCommendTests : CommandTestBase
{

    [Fact]
    public async Task Handler_Should_Return_Success_Result()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        var mediatorMock = new Mock<ISender>();

        var request = new CreateReservationCommand
        {
            CustomerName = "Edward",
            From = date,
            To = date.AddDays(1)
        };
        mediatorMock.Setup(x => x.Send(new IsBookingSpaceAvailableQuery(request.From, request.To), CancellationToken.None))
           .ReturnsAsync(true);
        var sut = new CreateReservationCommandHandler(_context, mediatorMock.Object);
        //Act
        var result = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        mediatorMock.Verify(m => m.Send(new IsBookingSpaceAvailableQuery(request.From, request.To), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handler_Should_Return_Failed_Result()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(3));
        var mediatorMock = new Mock<ISender>();
        var request = new CreateReservationCommand
        {
            CustomerName = "Edward",
            From = date,
            To = date.AddDays(1)
        };
        mediatorMock.Setup(x => x.Send(new IsBookingSpaceAvailableQuery(request.From, request.To), CancellationToken.None))
         .ReturnsAsync(false);
        var sut = new CreateReservationCommandHandler(_context, mediatorMock.Object);
        //Act
        var result = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        Assert.True(result.Errors.Any());
    }

    [Fact]
    public async Task Handler_Should_Throw_ArgumentNullException()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(3));
        var mediatorMock = new Mock<ISender>();
        var request = new CreateReservationCommand();
        request = null;
        var sut = new CreateReservationCommandHandler(_context, mediatorMock.Object);
        //Act
        var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.Handle(null, CancellationToken.None));



        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Message);
        mediatorMock.Verify(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Never);

    }
}
