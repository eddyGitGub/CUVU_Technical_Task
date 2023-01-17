using CUVU_Technical_Task.Application.Reservation.Commands.CreateReservation;
using WebUI.IntegrationTests.Common;

namespace WebUI.IntegrationTests.Controllers;
public class ReservationsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ReservationsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenCreateBookingCommand_ReturnsSuccessStatusCode()
    {
        var client = _factory.CreateClient();
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        var command = new CreateReservationCommand
        {
            CustomerName = "ABCDE",
            From = date,
            To = date.AddDays(1),

        };

        var content = Utilities.GetRequestContent(command);

        var response = await client.PostAsync($"/api/Reservations/CreateBooking", content);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenGetAvailableFreeSpaceQuery_ReturnsSuccessStatusCode()
    {
        var client = _factory.CreateClient();
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var response = await client.GetAsync($"/api/Reservations/GetAvalability?DateFrom={date:yyyy-MM-dd}&DateTo={date.AddDays(1):yyyy-MM-dd}");

        response.EnsureSuccessStatusCode();

        var result = await Utilities.GetResponseContent<List<string>>(response);
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
    }

    [Fact]
    public async Task GivenGetParkingPrice_ReturnsSuccessStatusCode()
    {
        var client = _factory.CreateClient();
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var response = await client.GetAsync($"/api/Reservations/GetParkingPrice?From={date:yyyy-MM-dd}&To={date.AddDays(1):yyyy-MM-dd}");

        response.EnsureSuccessStatusCode();

        var result = await Utilities.GetResponseContent<List<string>>(response);
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
    }

}
