using CUVU_Technical_Task.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CUVU_Technical_Task.Application.Parking.Commands.UpdateParkingSpace;

public class UpdateParkingCommandValidator : AbstractValidator<UpdateParkingSpaceCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateParkingCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.Id)
            .GreaterThan(0);

        RuleFor(v => v.Capacity)
          .GreaterThan(0)
          .MustAsync(BeGreaterThanUsedSpace).WithMessage("Capacity can't be updated");
    }

    private async Task<bool> BeGreaterThanUsedSpace(UpdateParkingSpaceCommand model,int arg1, CancellationToken cancellationToken)
    {
        var test= await _context.CarParks.Where(l => l.Id == model.Id)
                .AllAsync(l => l.UsedSpace <= arg1, cancellationToken);
        return test;
    }

    //public async Task<bool> BeGreaterThanUsedSpace(UpdateParkingSpaceCommand model, string capacity, CancellationToken cancellationToken)
    //{
    //    //return await _context.CarParks
    //    //    .Where(l => l.Id != model.Id)
    //    //    .AllAsync(l => l.Title != title, cancellationToken);
    //    return true;
    //}
}
