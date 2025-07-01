using CarRental.Domain.Enums;

namespace CarRental.Application.Models
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        public CarModel Model { get; set; }
        public int Year { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
