using BookingRooms.Domain;
using Microsoft.EntityFrameworkCore;
using System;


namespace BookingRooms.Infrastructure
{
    public class BookingReposirory : IBookingRepository
    {
        private readonly AppDBContext _dbContext;

        public BookingReposirory(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public List<Booking>? GetAll() => _dbContext.Bookings
            .Include(b=>b.Room)
            .Include(b=>b.User)
            .ToList();

        public Booking? GetById(Guid id) => _dbContext.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .FirstOrDefault(b => b.Id == id);


        public void Add(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
        }
    }
}
