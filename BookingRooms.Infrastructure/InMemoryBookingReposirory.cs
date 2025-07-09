using BookingRooms.Domain;
using System;


namespace BookingRooms.Infrastructure
{
    public class InMemoryBookingReposirory : IBookingRepository
    {
        private readonly List<Booking> _bookings = new();

        public List<Booking>? GetAll() => _bookings;

        public Booking GetById(Guid id) => _bookings.FirstOrDefault(b => b.Id == id);

        public void Add(Booking booking)=>_bookings.Add(booking);
    }
}
