using CarRental.Domain.Enums;

namespace CarRental.Application.Models.CreationDTOs
{
    public class CarCreationDTO
    {
        public CarModel Model { get; set; }
        public int Year { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
