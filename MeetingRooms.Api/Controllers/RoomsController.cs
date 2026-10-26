
using Microsoft.AspNetCore.Mvc;

namespace MeetingRooms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRooms()
    {
        var rooms = new[]
        {
            new { Id = 1, Name = "Meeting Room A" },
            new { Id = 2, Name = "Meeting Room B" },
            new { Id = 3, Name = "Meeting Room C" }
        };

        return Ok(rooms);
    }
}
