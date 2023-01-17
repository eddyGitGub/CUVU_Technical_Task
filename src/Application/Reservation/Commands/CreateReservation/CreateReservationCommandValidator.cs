using FluentValidation;

namespace CUVU_Technical_Task.Application.Reservation.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(v => v.CustomerName)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.From)
           .NotEmpty()
           .Must(BeAValidDate).WithMessage("From date is required")
           .Must(IsNotPastDate).WithMessage("Past date not allowed");

        RuleFor(v => v.To)
          .NotEmpty()
          .Must(BeAValidDate).WithMessage("To date is required");

        RuleFor(x => x).Must(x => x.To == default || x.From == default || x.To >= x.From)
        .WithMessage("To date must greater than From date");

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
