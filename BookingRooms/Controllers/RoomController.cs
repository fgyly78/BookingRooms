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

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(Guid id, [FromBody] User user)
        {
            try
            {
                _roomService.DeleteRoom(id, user);
                return NoContent(); // 204
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
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
