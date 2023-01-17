namespace CUVU_Technical_Task.Domain.Entities;
public class Booking : BaseAuditableEntity
{
    public string CustomerName { get; set; } = string.Empty;
    public DateOnly DateTo { get; set; }
    public DateOnly DateFrom { get; set; }
    public int DurationInDay { get; set; }
    public bool IsCancel { get; set; } = false;
}
