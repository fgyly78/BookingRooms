using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Domain
{
    public class Room
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public RoomState State { get; set; }
    }

    public enum RoomState
    {
        Available,
        Occupied
    }
}
