namespace CarRental.Application.Models.CreationDTOs
{
    public class BookingCreationDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public int NumberOfKm { get; set; }
    }
}
