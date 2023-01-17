using System.Runtime.Serialization;
using AutoMapper;
using CUVU_Technical_Task.Application.Common.Mappings;
using CUVU_Technical_Task.Application.Parking.Queries;
using CUVU_Technical_Task.Application.Reservation.Queries.GetBooking;
using CUVU_Technical_Task.Domain.Entities;

namespace Application.UnitTest.Common.Mapping;
public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

   
    [Theory]
    [InlineData(typeof(Booking), typeof(BookingVm))]
    [InlineData(typeof(CarPark), typeof(CarParkVm))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}