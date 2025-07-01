using CarRental.Domain.Enums;

namespace CarRental.Application.Models
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarCatagory CarCatagory { get; set; }
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public string CarRegistrationNumber { get; set; } = string.Empty;
        public decimal RentalPrice { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfKm { get; set; }
    }
}
