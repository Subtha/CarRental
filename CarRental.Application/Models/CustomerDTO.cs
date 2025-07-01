namespace CarRental.Application.Models
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string SSN { get; set; } = string.Empty;
    }
}
