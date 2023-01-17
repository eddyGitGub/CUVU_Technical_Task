using CUVU_Technical_Task.Application.Common.Interfaces;
using CUVU_Technical_Task.Domain.Entities;
using CUVU_Technical_Task.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTest.Common;
public class ApplicationDbContextFactory
{

    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var mediator = new Mock<IMediator>();
        var currentUser = new Mock<ICurrentUserService>();
        var dateTime = new Mock<IDateTime>();
        var interceptor = new AuditableEntitySaveChangesInterceptor(currentUser.Object, dateTime.Object);

        var context = new ApplicationDbContext(options, mediator.Object, interceptor);

        context.Database.EnsureCreated();
        // Seed, if necessary
        if (!context.CarParks.Any())
        {
            context.CarParks.Add(new CarPark
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
            });
            context.SaveChanges();
        }

        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}

