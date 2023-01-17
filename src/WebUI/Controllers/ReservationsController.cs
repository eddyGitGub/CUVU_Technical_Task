using CUVU_Technical_Task.Application.Common.Models;
using CUVU_Technical_Task.Application.Parking.Queries;
using CUVU_Technical_Task.Application.Reservation.Commands.CancelReservation;
using CUVU_Technical_Task.Application.Reservation.Commands.CreateReservation;
using CUVU_Technical_Task.Application.Reservation.Commands.UpdateReservation;
using CUVU_Technical_Task.Application.Reservation.Queries.GetAvailableParkingSpace;
using CUVU_Technical_Task.Application.Reservation.Queries.GetBooking;
using CUVU_Technical_Task.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ApiControllerBase
{

    [HttpGet("GetAllBooking")]
    public async Task<ActionResult<List<BookingVm>>> GetAllBooking()
    {
        var query = new GetBookingsQuery();
        return await Mediator.Send(query);
    }
    [HttpGet("GetAvalability")]
    public async Task<ActionResult<List<string>>> GetAvalability([FromQuery] GetAvailableFreeSpaceQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("GetParkingPrice")]
    public async Task<ActionResult<List<string>>> GetParkingPrice([FromQuery] GetParkingPriceQuery query)
    {
        return await Mediator.Send(query);
    }
    [HttpPost("CreateBooking")]
    public async Task<ActionResult<Result>> CreateBooking(CreateReservationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> AmendBookingAsync(int id, UpdateReservationCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("Cancel/{id}")]
    public async Task<ActionResult<Result>> CancelBooking(int id, CancelReservationCommand command)
    {
        return id != command.Id ? (ActionResult<Result>)BadRequest() : (ActionResult<Result>)await Mediator.Send(command);
    }
}
