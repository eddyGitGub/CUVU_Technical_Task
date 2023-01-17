using FluentValidation;

namespace CUVU_Technical_Task.Application.Reservation.Commands.CancelReservation;

public class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand>
{
    public CancelReservationCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);

    }
}
