
namespace MeetingRooms.Api.DTOs;
public class CreateBookingRequest
{
    public int RoomId { get; set; }
    public string? Title { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? BookedBy { get; set; }
}
