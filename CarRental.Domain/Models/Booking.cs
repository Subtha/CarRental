using CarRental.Domain.Enums;

namespace CarRental.Domain.Models
{
    public class Booking
    {
        /// <summary>Guid er bookingNumber/// </summary>
        public Guid Id { get; set; }

        /// <summary>Dato booking ble registrert - i utc format./// </summary>
        public DateTime BookingDate { get; set; }   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CarCatagory CarCatagory { get; set; }
        public string CarRegistrationNumber { get; set; } = string.Empty;
        public Guid CarId { get; set; }

        public Guid CustomerId { get; set; }

        /// <summary>Cusomter Soscial Security Number (Personnummer)/// </summary>
        public string CustomerSSN { get; set; } = string.Empty;

        public decimal RentalPrice { get; set; }

        /// <summary>calculated value fra Start og end dates./// </summary>
        public int NumberOfDays { get; set; } 
        public int NumberOfKm { get; set; } //Input parameter fra bruker.

        public Booking()
        {
            Id = Guid.NewGuid();
            BookingDate = DateTime.UtcNow;
        }
    }
}
