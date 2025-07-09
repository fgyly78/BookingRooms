using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Domain
{
    public interface IBookingRepository
    {
        List<Booking>? GetAll();
        Booking GetById(Guid id);
        void Add(Booking booking);
    }
}
