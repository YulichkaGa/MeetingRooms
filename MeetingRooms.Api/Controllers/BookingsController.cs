
using Microsoft.AspNetCore.Mvc;
using MeetingRooms.Api.DTOs;
using MeetingRooms.Api.Services;

namespace MeetingRooms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public IActionResult GetBookings(
        [FromQuery] int roomId,
        [FromQuery] DateOnly date)
    {
        if (roomId < 1 || date == default)
            return BadRequest("Valid roomId and date are required.");

        return Ok(_bookingService.GetBookings(roomId, date));
    }

    [HttpPost]
    public IActionResult CreateBooking(
        [FromBody] CreateBookingRequest request)
    {
        try
        {
            var booking = _bookingService.CreateBooking(request);
            return StatusCode(StatusCodes.Status201Created, booking);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult CancelBooking(Guid id)
    {
        return _bookingService.CancelBooking(id)
            ? NoContent()
            : NotFound(new { message = "Booking not found." });
    }
}
