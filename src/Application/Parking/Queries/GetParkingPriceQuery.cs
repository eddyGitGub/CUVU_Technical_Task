using CUVU_Technical_Task.Application.Extensions;
using FluentValidation;
using MediatR;

namespace CUVU_Technical_Task.Application.Parking.Queries;

public record GetParkingPriceQuery(DateOnly From, DateOnly To) : IRequest<List<string>>;
public class GetParkingPriceQueryValidator : AbstractValidator<GetParkingPriceQuery>
{
    public GetParkingPriceQueryValidator()
    {
        RuleFor(v => v.From)
       .NotEmpty()
       .Must(BeAValidDate).WithMessage("From date is invalid")
       .Must(IsNotPastDate).WithMessage("Past date not allowed");

        RuleFor(v => v.To)
          .NotEmpty()
          .Must(BeAValidDate).WithMessage("To date is invalid");

        RuleFor(x => x).Must(x => x.To == default || x.From == default || x.To >= x.From)
        .WithMessage("FromDate must greater than or equal ToDate");
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
public class GetParkingPriceQueryHandler : IRequestHandler<GetParkingPriceQuery, List<string>>
{

    public async Task<List<string>> Handle(GetParkingPriceQuery request, CancellationToken cancellationToken)
    {
        List<string> parkingPrice = new();
        var requestDates = request.From.ToDates(request.To);
        for (int i = 0; i < requestDates.Count; i++)
        {
            var price = requestDates[i].CheckParkingPrice();
            parkingPrice.Add($"Parking Price for {requestDates[i]} is {price} pounds");
        }

        return await Task.FromResult(parkingPrice);
    }
}
