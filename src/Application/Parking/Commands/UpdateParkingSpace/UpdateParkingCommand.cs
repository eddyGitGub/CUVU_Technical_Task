using CUVU_Technical_Task.Application.Common.Exceptions;
using CUVU_Technical_Task.Application.Common.Interfaces;
using CUVU_Technical_Task.Application.Common.Models;
using CUVU_Technical_Task.Domain.Entities;
using MediatR;

namespace CUVU_Technical_Task.Application.Parking.Commands.UpdateParkingSpace;

public record UpdateParkingSpaceCommand(int Id, int Capacity) : IRequest<Result>;

public class UpdateParkingSpaceCommandHandler : IRequestHandler<UpdateParkingSpaceCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateParkingSpaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateParkingSpaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CarParks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CarPark), request.Id);
        }
        entity.Capacity = request.Capacity;
        var diff = entity.Capacity - entity.FreeSpace;
        if (diff > 0)
        {
            entity.FreeSpace += diff;
        }
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
