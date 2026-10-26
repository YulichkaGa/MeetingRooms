
namespace MeetingRooms.Api.Models;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int RoomId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string BookedBy { get; set; } = string.Empty;
}
