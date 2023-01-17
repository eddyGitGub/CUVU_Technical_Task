using CUVU_Technical_Task.Application.Common.Models;
using CUVU_Technical_Task.Application.Parking.Commands.UpdateParkingSpace;
using CUVU_Technical_Task.Application.Parking.Queries;
using CUVU_Technical_Task.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ParkingSpaceController : ApiControllerBase
{
    [HttpGet("GetCarParkSpace")]
    public async Task<ActionResult<CarParkVm>> GetCarPark()
    {
        var query = new GetParkingSpaceQuery();
        return await Mediator.Send(query);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(int id, UpdateParkingSpaceCommand command)
    {
        return id != command.Id ? (ActionResult<Result>)BadRequest() : (ActionResult<Result>)await Mediator.Send(command);
    }
}
