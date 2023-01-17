using CUVU_Technical_Task.Application.Parking.Queries;

namespace Application.UnitTest.Parking.Queries;
public class GetParkingPriceQueryTests
{


    [Theory]
    [InlineData(3, 20)]
    [InlineData(6, 25)]
    [InlineData(9, 30)]
    [InlineData(12, 35)]
    public async Task Handler_Should_ReturnValid_Price_For_Season(int month, double price)
    {
        var date = new DateOnly(DateTime.Now.Year, month, 1);
        //arrange 
        var query = new GetParkingPriceQuery(date, date);
        var handler = new GetParkingPriceQueryHandler();

        //Acct 
        var result = await handler.Handle(query, CancellationToken.None);
        var expectedResult = new List<string> { $"Parking Price for {date} is {price} pounds" };

        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);

    }
}
