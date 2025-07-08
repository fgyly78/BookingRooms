using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingRooms.Domain;

namespace BookingRooms.Application
{
    internal class BookingService
    {
        private readonly List<Booking> _bookings;
        private readonly List<User> _users;
        private readonly List<Room> _rooms;

        public BookingService(List<Booking> bookings, List<User> users, List<Room> rooms)
        {
            _bookings = bookings;
            _users = users;
            _rooms = rooms;
        }

        public List<Booking> GetBookingsForUser(Guid userId)
        {
            return _bookings.Where(b => b.UserId == userId).ToList();
        }

        public void CreateBooking(Booking booking, User currentUser)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == booking.RoomId);

            if (room == null) throw new Exception("Комната не найдена");

            if (room.State == RoomState.Occupied)
                throw new Exception("Комната уже занята");

            booking.Id = Guid.NewGuid();
            booking.UserId = currentUser.Id;
            booking.RoomId = room.Id;
            booking.StartDate = DateTime.Now;
            _bookings.Add(booking);
            room.State = RoomState.Occupied;
        }

        public string GetBookingInfo(Guid bookingId, User currentUser)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
            var room = _rooms.FirstOrDefault(r => r.Id == booking.RoomId);
            var user = _users.FirstOrDefault(u => u.Id == booking.UserId);

            if (booking == null)
            {
                throw new NullReferenceException("Бронирование не найдено!");
            }

            if (booking.UserId != currentUser.Id && currentUser.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Нет доступа к этой брони!");

            return $"Пользователь: {user?.FullName ?? "неизвестно"}, Комната: {room?.Number ?? -1}";
        }

        public void CancelBooking(Guid bookingId, User currentUser)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
                throw new NullReferenceException("Бронирование не найдено.");

            if (booking.UserId != currentUser.Id && currentUser.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Недостаточно прав для отмены брони.");

            if (booking.State == BookingState.Cancelled)
                throw new Exception("Бронь уже отменена");

            var room = _rooms.FirstOrDefault(r => r.Id == booking.RoomId);
            if (room != null)
            {
                booking.State = BookingState.Cancelled;
                room.State = RoomState.Avaliable;
            }
        }
    }
}
