using CUVU_Technical_Task.Application.Common.Interfaces;
using CUVU_Technical_Task.Application.Parking.Queries;

namespace Application.UnitTest.Parking.Queries;
[Collection("QueryCollection")]
public class GetParkingSpaceQueryTests
{
    private readonly IApplicationDbContext _context;

    public GetParkingSpaceQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public async Task Handler_ShouldReturn_Result()
    {
        var query = new GetParkingSpaceQuery();
        var handler = new GetParkingSpaceQueryHandler(_context);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(10, result.Capacity);
    }
}
