using BookingRooms.Domain;
using System;
using Microsoft.EntityFrameworkCore;


namespace BookingRooms.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _dbContext;

        public UserRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public User? GetUserById(Guid id)=> _dbContext.Users.Find(id);
    }
}
