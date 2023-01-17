using CUVU_Technical_Task.Application.Common.Interfaces;

namespace CUVU_Technical_Task.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
