using CarRental.Application.Interfaces;
using CarRental.Application.Models;
using CarRental.Application.Models.CreationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService), "Bookingservice can not be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger can not be null");
        }

        [HttpGet("GetBookingById/{id}", Name = "GetBookingById")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Booking id is empty");
            }

            var booking = await _bookingService.GetBookingById(id);
            if (!booking.Success)
            {
                _logger.LogWarning(booking.Errors.First());
                return NotFound(booking.Errors.First());
            }
            return Ok(booking.Data);
        }

        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookings();
            if(bookings.Count() == 0)
            {
                return NotFound("No bookings found.");
            }

            return Ok(bookings);
        }

        [HttpPost("CreateBooking")]
        public async Task<ActionResult<BookingDTO>> CreateBooking(BookingCreationDTO bookingCreationDTO)
        {
            var result = await _bookingService.CreateBooking(bookingCreationDTO);

            if(result == null)
            {
                return StatusCode(500, new
                {
                    Message = "Internal error. BookingService returned null result."
                });
            }

            if (!result.Success)
            {
                return BadRequest(new 
                { 
                    Message = "Booking validation failed",
                    ErrorMessages = result.Errors
                });
            }

            if (result.Data == null)
            {
                return StatusCode(500, new
                {
                    Message = "Internal error. Booking data is null after Result.Success."
                });
            }

            return CreatedAtRoute("GetBookingById",
                new
                {
                    id = result.Data.Id
                },
                result.Data
            );

        }

    }
}
