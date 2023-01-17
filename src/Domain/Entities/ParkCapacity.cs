namespace CUVU_Technical_Task.Domain.Entities;
public class CarPark : BaseAuditableEntity
{
    public DateOnly Date { get; set; }
    public int Capacity { get; set; } = 10;
    public int FreeSpace { get; set; }
    public int UsedSpace { get; set; }
    public bool IsActive { get; set; }=true;
    public CarPark()
    {
        FreeSpace= Capacity;
    }

}
