using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Domain
{
    public interface IRoomRepository
    {
        Room? GetRoomById(Guid id);
        void Update(Room room);
    }
}
