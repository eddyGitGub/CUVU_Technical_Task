using AutoMapper;
using CUVU_Technical_Task.Application.Common.Exceptions;
using CUVU_Technical_Task.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUVU_Technical_Task.Application.Parking.Queries;
public record GetParkingSpaceQuery : IRequest<CarParkVm>;

public class GetParkingSpaceQueryHandler : IRequestHandler<GetParkingSpaceQuery, CarParkVm>
{
    private readonly IApplicationDbContext _context;
    public GetParkingSpaceQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CarParkVm> Handle(GetParkingSpaceQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.CarParks.FirstOrDefaultAsync(x => x.IsActive, cancellationToken: cancellationToken);
        return result == null
            ? throw new NotFoundException(nameof(CarParkVm))
            : new CarParkVm { Capacity = result.Capacity, Id = result.Id, FreeSpace = result.FreeSpace };
    }
}
