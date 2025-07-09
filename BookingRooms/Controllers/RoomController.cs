using Microsoft.AspNetCore.Mvc;
using BookingRooms.Application;
using BookingRooms.Domain;

namespace BookingRooms.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("Create Room")]
        public IActionResult CreateRoom([FromBody] CreateRoomRequest request)
        {
            try
            {
                _roomService.CreateRoom(request.Room, request.User);
                return Ok(request.Room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("available")]
        public ActionResult<List<Room>> GetAvailable()
        {
            return _roomService.GetAvailableRooms();
        }
    }

    public class CreateRoomRequest
    {
        public Room Room { get; set; }
        public User User { get; set; }
    }
}
