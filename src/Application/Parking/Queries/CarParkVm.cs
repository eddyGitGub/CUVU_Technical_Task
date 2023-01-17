using CUVU_Technical_Task.Application.Common.Mappings;
using CUVU_Technical_Task.Domain.Entities;

namespace CUVU_Technical_Task.Application.Parking.Queries;
public class CarParkVm : IMapFrom<CarPark>
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int Capacity { get; set; }
    public int FreeSpace { get; set; }
    public int UsedSpace { get; set; }
    public bool IsActive { get; set; }
}
