using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }

        public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }

        public BookingState State { get; set; }
    }

    public enum BookingState
    {
        Confirmed,
        Cancelled
    }
}
