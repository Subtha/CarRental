namespace CarRental.Application.Models.CreationDTOs
{
    public class CustomerCreationDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string SSN { get; set; } = string.Empty;
    }
}
