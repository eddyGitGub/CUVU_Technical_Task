using CUVU_Technical_Task.Application.Parking.Commands.UpdateParkingSpace;

namespace Application.UnitTest.Parking.Commands;
public class UpdateParkingSpaceCommandTests : CommandTestBase
{
    [Fact]
    public async Task Handler_Should_Update_And_Returns_Success()
    {
        var command = new UpdateParkingSpaceCommand(1, 20);
        var handler = new UpdateParkingSpaceCommandHandler(_context);

        var result = await handler.Handle(command, CancellationToken.None);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }
}
