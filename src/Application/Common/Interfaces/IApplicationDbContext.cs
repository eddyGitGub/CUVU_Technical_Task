using CUVU_Technical_Task.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CUVU_Technical_Task.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Booking> Bookings { get; }
    DbSet<CarPark> CarParks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
