using BookingRooms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Infrastructure
{
    public class InMemoryRoomRepository : IRoomRepository
    {
        private readonly List<Room> _rooms;

        public Room? GetRoomById(Guid id) => _rooms.FirstOrDefault(r => r.Id == id);

        public void Update(Room updatedRoom)
        {
            var existingRoom = _rooms.FirstOrDefault(r => r.Id == updatedRoom.Id);
            if (existingRoom == null)
                throw new Exception("Комната не найдена");

            existingRoom.Number = updatedRoom.Number;
            existingRoom.PricePerNight = updatedRoom.PricePerNight;
            existingRoom.State = updatedRoom.State;
        }
    }
}
