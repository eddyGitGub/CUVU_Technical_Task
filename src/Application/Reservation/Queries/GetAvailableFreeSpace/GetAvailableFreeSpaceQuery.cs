using CUVU_Technical_Task.Application.Common.Interfaces;
using CUVU_Technical_Task.Application.Parking.Queries;
using FluentValidation;
using MediatR;

namespace CUVU_Technical_Task.Application.Reservation.Queries.GetAvailableParkingSpace;

public record GetAvailableFreeSpaceQuery(DateOnly DateFrom, DateOnly DateTo) : IRequest<List<string>>;

public class GetAvailableFreeSpaceQueryValidator : AbstractValidator<GetAvailableFreeSpaceQuery>
{
    public GetAvailableFreeSpaceQueryValidator()
    {
        RuleFor(v => v.DateFrom)
       .NotEmpty()
       .Must(BeAValidDate).WithMessage("DateFrom is required");

        RuleFor(v => v.DateTo)
          .NotEmpty()
          .Must(BeAValidDate).WithMessage("DateTo is required")
          .Must(IsNotPastDate).WithMessage("Past date not allowed");

        RuleFor(x => x).Must(x => x.DateTo == default || x.DateFrom == default || x.DateTo >= x.DateFrom)
        .WithMessage("FromDate must greater than or equal to ToDate");
    }

    private bool BeAValidDate(DateOnly date)
    {
        return !date.Equals(default);
    }
    private bool IsNotPastDate(DateOnly date)
    {
        DateOnly d = DateOnly.FromDateTime(DateTime.Now);
        return date.CompareTo(d) >= 0;
    }
}
public class GetAvailableFreeSpaceQueryHandler : IRequestHandler<GetAvailableFreeSpaceQuery, List<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _sender;
    public GetAvailableFreeSpaceQueryHandler(IApplicationDbContext context, ISender sender)
    {
        _context = context;
        _sender = sender;
    }

    public async Task<List<string>> Handle(GetAvailableFreeSpaceQuery request, CancellationToken cancellationToken)
    {
        var parkingSpace = await _sender.Send(new GetParkingSpaceQuery(), cancellationToken);
        var totalSpace = parkingSpace?.Capacity ?? 10;
        var dateFrom = request.DateFrom;
        var dateTo = request.DateTo;

        var bookedDates = _context.Bookings
             .AsEnumerable()
            .Where(b => (b.DateFrom <= dateFrom && b.DateTo >= dateFrom && !b.IsCancel) ||
                        (b.DateFrom <= dateTo && b.DateTo >= dateTo && !b.IsCancel) ||
                        (b.DateFrom >= dateFrom && b.DateTo <= dateTo && !b.IsCancel))
                                .SelectMany(b => Enumerable.Range(0, (b.DateTo.DayNumber - b.DateFrom.DayNumber) + 1)
                                .Select(d => b.DateFrom.AddDays(d)))
                                .ToList();

        var freeDates = Enumerable.Range(0, (dateTo.DayNumber - dateFrom.DayNumber) + 1)
                                  .Select(d => dateFrom.AddDays(d))
                                  .Select(date => $"- {date:dd/MM/yyyy} - {(totalSpace - bookedDates.Count(d => d == date))} free spaces")
                                  .ToList();
        return freeDates;

    }
}
