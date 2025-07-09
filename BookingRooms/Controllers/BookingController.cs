using Microsoft.AspNetCore.Mvc;
using BookingRooms.Application;
using BookingRooms.Domain;

namespace BookingRooms.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public ActionResult<List<Booking>> GetBookings([FromQuery] Guid userId)
        {
            return _bookingService.GetBookingsForUser(userId);
        }

        [HttpPost("getinfo")]
        public IActionResult GetBookingInfo([FromBody] BookingInfoRequest request)
        {
            try
            {
                var info = _bookingService.GetBookingInfo(request.BookingId, request.User);
                return Ok(info);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateBookingRequest request)
        {
            _bookingService.CreateBooking(request.Booking, request.User);
            return CreatedAtAction(nameof(GetBookingInfo), new { id = request.Booking.Id }, request.Booking);
        }

        [HttpPost("cancel")]
        public IActionResult CancelBooking([FromBody] CancelBookingRequest request)
        {
            try
            {
                _bookingService.CancelBooking(request.BookingId, request.User);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

    public class CreateBookingRequest
    {
        public Booking Booking { get; set; }
        public User User { get; set; }
    }

    public class BookingInfoRequest
    {
        public Guid BookingId { get; set; }
        public User User { get; set; }
    }

    public class CancelBookingRequest
    {
        public Guid BookingId { get; set; }
        public User User { get; set; }
    }
}
