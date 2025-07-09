using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using BookingRooms.Domain;

namespace BookingRooms.Application
{
    internal class RoomService
    {
        private readonly List<Room> _rooms;

        public RoomService(List<Room> rooms)
        {
            _rooms = rooms;
        }

        public void CreateRoom(Room room, User currentUser)
        {
            if (currentUser.Role != UserRole.Admin)
                throw new Exception("Только Администратор может создать комнату!");

            room.Id = Guid.NewGuid();
            room.State = RoomState.Available;
            _rooms.Add(room);
        }

        public void DeleteRoom(Guid roomId, User currentUser)
        {
            if (currentUser.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Недостаточно прав.");

            var room = _rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
                throw new NullReferenceException("Комната не найдена.");

            if (room.State == RoomState.Occupied)
                throw new Exception("Нельзя удалить занятую комнату.");

            _rooms.Remove(room);
        }

        public List<Room> GetAvailableRooms()
        {
            return _rooms.Where(r => r.State == RoomState.Available).ToList();
        }

        public Room? GetRoomById(Guid id)
        {
            return _rooms.FirstOrDefault(r => r.Id == id);
        }
    }
}
