using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingRooms.Domain;
using BookingRooms.Infrastructure;

namespace BookingRooms.Application
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly AppDBContext _appDBContext;

        public BookingService(IBookingRepository bookingRepository, AppDBContext appDBContext)
        {
            _bookingRepository = bookingRepository;
            _appDBContext = appDBContext;
        }

        public List<Booking> GetBookingsForUser(Guid userId)
        {
            return _bookingRepository.GetAll().Where(b => b.UserId == userId).ToList();
        }

        public void CreateBooking(Booking booking, User user)
        {
            var room = _appDBContext.Rooms.FirstOrDefault(r => r.Id == booking.Room.Id);

            if (room == null)
                throw new Exception("Комната не найдена");

            if (room.State == RoomState.Occupied)
                throw new Exception("Комната уже занята");


            if (room.Id == Guid.Empty)
                room.Id = Guid.NewGuid();

            booking.Room = room; // используй загруженную из БД комнату
            booking.User = user;
            booking.StartDate = DateTime.UtcNow;
            booking.EndDate = DateTime.UtcNow.AddDays(1);

            room.State = RoomState.Occupied;

            _appDBContext.Bookings.Add(booking);
            _appDBContext.SaveChanges();
        }


        public string GetBookingInfo(Guid bookingId, User currentUser)
        {
            var booking = _bookingRepository.GetById(bookingId);

            if (booking == null)
            {
                throw new NullReferenceException("Бронирование не найдено!");
            }

            if (booking.UserId != currentUser.Id && currentUser.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Нет доступа к этой брони!");

            return $"Пользователь: {booking.User?.FullName ?? "неизвестно"}, Комната: {booking.Room?.Number ?? -1}";
        }

        public void CancelBooking(Guid bookingId, User currentUser)
        {
            var booking = _bookingRepository.GetById(bookingId);
            if (booking == null)
                throw new NullReferenceException("Бронирование не найдено.");

            if (booking.UserId != currentUser.Id && currentUser.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Недостаточно прав для отмены брони.");

            if (booking.State == BookingState.Cancelled)
                throw new Exception("Бронь уже отменена");

            var room = _appDBContext.Rooms.FirstOrDefault(r => r.Id == booking.RoomId);
            if (room != null)
            {
                booking.State = BookingState.Cancelled;
                room.State = RoomState.Available;
                _appDBContext.SaveChanges();
            }
        }
    }
}
