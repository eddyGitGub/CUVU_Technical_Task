namespace CUVU_Technical_Task.Domain.Events;

public class BookingCreatedEvent : BaseEvent
{
    public BookingCreatedEvent(Booking item)
    {
        Item = item;
    }

    public Booking Item { get; }
}
