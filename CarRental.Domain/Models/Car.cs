using CarRental.Domain.Enums;

namespace CarRental.Domain.Models
{
    public class Car
    {
        /// <summary>Unik Id som brukes for å identifisere bilen i systemet./// </summary>
        public Guid Id { get; set; }
        
        /// <summary>Modell navn av bilen , eks. BMW, Audi, Tesla osv./// </summary>
        public CarModel Model { get; set; }
        
        /// <summary>Bilens årsmodell/// </summary>
        public int Year { get; set; } 
        public string RegistrationNumber { get; set; }
        public bool IsAvailable { get; set; }
        
        /// <summary>Bil katagori: small, combi, truck./// </summary>
        public CarCatagory CarCatagory { get; set; }


        public Car(CarModel carModel, int year, string registrationNumber, CarCatagory carCatagory)
        {
            Id = Guid.NewGuid();
            Model = carModel;
            Year = year;
            RegistrationNumber = registrationNumber;
            IsAvailable = true;
            CarCatagory = carCatagory;
        }
    }
}
