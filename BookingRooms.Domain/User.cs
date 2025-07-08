using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        User,
        Admin
    }
}
