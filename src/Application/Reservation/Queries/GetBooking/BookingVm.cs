using CUVU_Technical_Task.Application.Common.Mappings;
using CUVU_Technical_Task.Domain.Entities;

namespace CUVU_Technical_Task.Application.Reservation.Queries.GetBooking;

public class BookingVm : IMapFrom<Booking>
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public int DurationInDay { get; set; }
    public bool IsCancel { get; set; }
}
