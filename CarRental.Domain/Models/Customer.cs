namespace CarRental.Domain.Models
{
    public class Customer
    {
        /// <summary>Unik Id for hver kunde. Brukes for å identifisere kunden i systemet./// </summary>
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        
        /// <summary>Social Security Number (Personnummer)/// </summary>
        public string SSN { get; set; } = string.Empty;
        
        
        public Customer(string name, string email, string phoneNumber, string ssn)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            SSN = ssn;
        }
    }
}
