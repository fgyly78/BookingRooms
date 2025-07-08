using BookingRooms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Application
{
    internal class UserService
    {
        private readonly List<User> _users;

        public UserService(List<User> users)
        {
            _users = users;
        }

        public void RegisterUser(User user)
        {
            if (_users.Any(u => u.FullName == user.FullName))
                throw new Exception("Пользователь с таким именем уже существует.");

            user.Id = Guid.NewGuid();
            user.Role = UserRole.User;
            _users.Add(user);
        }

        public User? GetUserById(Guid userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public List<User> GetAllUsers(User currentUser)
        {
            if (currentUser.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Только администратор может просматривать список пользователей.");

            return _users;
        }
    }

}
