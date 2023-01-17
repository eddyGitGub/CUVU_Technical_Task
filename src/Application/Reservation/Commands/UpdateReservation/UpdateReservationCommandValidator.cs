using FluentValidation;

namespace CUVU_Technical_Task.Application.Reservation.Commands.UpdateReservation;

public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
{
    public UpdateReservationCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);

        RuleFor(v => v.From)
           .NotNull();

        RuleFor(v => v.To)
          .NotNull();

        RuleFor(x => x).Must(x => x.To == default || x.From == default || x.To >= x.From)
        .WithMessage("To Date must greater than From Date");
    }
}
