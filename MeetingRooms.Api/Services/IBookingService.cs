
using MeetingRooms.Api.DTOs;
using MeetingRooms.Api.Models;

namespace MeetingRooms.Api.Services;

public interface IBookingService
{
    IReadOnlyList<Booking> GetBookings(int roomId, DateOnly date);
    Booking CreateBooking(CreateBookingRequest request);
    bool CancelBooking(Guid id);
}
