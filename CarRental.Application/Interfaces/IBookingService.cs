using CarRental.Application.Common;
using CarRental.Application.Models;
using CarRental.Application.Models.CreationDTOs;

namespace CarRental.Application.Interfaces
{
    public interface IBookingService
    {
        Task<OperationResult<BookingDTO>> GetBookingById(Guid id);
        Task<OperationResult<BookingDTO>> CreateBooking(BookingCreationDTO bookingCreationDTO);
        Task<IEnumerable<BookingDTO>> GetAllBookings();
    }
}
