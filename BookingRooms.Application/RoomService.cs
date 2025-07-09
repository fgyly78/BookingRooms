using BookingRooms.Domain;
using BookingRooms.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Application
{
    public class RoomService
    {
        private readonly AppDBContext _db;
        private readonly IRoomRepository _roomRepository;

        public RoomService(AppDBContext context, IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
            _db = context;
        }

        public void CreateRoom(Room room, User currentUser)
        {
            if (currentUser.Role != UserRole.Admin)
                throw new Exception("Только Администратор может создать комнату!");

            if (room.Id == Guid.Empty)
                room.Id = Guid.NewGuid();
            room.State = RoomState.Available;
            _db.Rooms.Add(room);
            _db.SaveChanges();  
        }

        public void DeleteRoom(Guid roomId, User currentUser)
        {
            if (currentUser.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Недостаточно прав.");

            var room = _db.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
                throw new NullReferenceException("Комната не найдена.");

            if (room.State == RoomState.Occupied)
                throw new Exception("Нельзя удалить занятую комнату.");

            _db.Rooms.Remove(room);
        }

        public List<Room> GetAvailableRooms()
        {
            return _db.Rooms.Where(r => r.State == RoomState.Available).ToList();
        }

        public Room? GetRoomById(Guid id)
        {
            return _db.Rooms.FirstOrDefault(r => r.Id == id);
        }
    }
}
