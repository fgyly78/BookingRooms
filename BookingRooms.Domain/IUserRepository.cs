using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Domain
{
    public interface IUserRepository
    {
        User? GetUserById(Guid id);
    }
}
