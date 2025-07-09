using BookingRooms.Domain;
using System;


namespace BookingRooms.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public User? GetUserById(Guid id)=>_users.FirstOrDefault(u=>u.Id == id);
    }
}
