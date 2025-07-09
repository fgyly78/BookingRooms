using BookingRooms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BookingRooms.Infrastructure
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDBContext _dbContext;

        public RoomRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Room? GetRoomById(Guid id) => _dbContext.Rooms
            .Find(id);

        public void Update(Room updatedRoom)
        {
            var existingRoom = _dbContext.Rooms.FirstOrDefault(r => r.Id == updatedRoom.Id);
            if (existingRoom == null)
                throw new Exception("Комната не найдена");

            existingRoom.Number = updatedRoom.Number;
            existingRoom.PricePerNight = updatedRoom.PricePerNight;
            existingRoom.State = updatedRoom.State;
        }
    }
}
