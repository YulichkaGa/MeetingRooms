
using MeetingRooms.Api.DTOs;
using MeetingRooms.Api.Models;

namespace MeetingRooms.Api.Services;

public class BookingService : IBookingService
{
    private readonly List<Booking> _bookings = new();
    private readonly object _lock = new();

    public IReadOnlyList<Booking> GetBookings(int roomId, DateOnly date)
    {
        lock (_lock)
        {
            return _bookings
                .Where(b => b.RoomId == roomId &&
                    DateOnly.FromDateTime(b.Start) == date)
                .OrderBy(b => b.Start)
                .ToList();
        }
    }


    public Booking CreateBooking(CreateBookingRequest request)
    {
        if (request.RoomId is < 1 or > 3)
            throw new ArgumentException("Invalid room.");

        if (string.IsNullOrWhiteSpace(request.Title) ||
            string.IsNullOrWhiteSpace(request.BookedBy))
            throw new ArgumentException("Title and BookedBy are required.");

        if (request.End <= request.Start ||
            request.Start.Date != request.End.Date)
            throw new ArgumentException("Invalid booking time range.");

        lock (_lock)
        {
            bool overlaps = _bookings.Any(b =>
                b.RoomId == request.RoomId &&
                b.Start < request.End &&
                request.Start < b.End);

            if (overlaps)
                throw new InvalidOperationException(
                    "Room is already booked for this time range.");

            var booking = new Booking
            {
                RoomId = request.RoomId,
                Title = request.Title.Trim(),
                Start = request.Start,
                End = request.End,
                BookedBy = request.BookedBy.Trim()
            };

            _bookings.Add(booking);
            return booking;
        }
    }


    public bool CancelBooking(Guid id)
    {
        lock (_lock)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == id);
            return booking != null && _bookings.Remove(booking);
        }
    }
}
