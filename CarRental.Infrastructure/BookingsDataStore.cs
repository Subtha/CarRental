using CarRental.Domain.Enums;
using CarRental.Domain.Models;

namespace CarRental.Infrastructure
{
    public class BookingsDataStore
    {
        public List<Booking> Bookings { get; set; }
        public List<Car> Cars { get; set; } 
        public List<Customer> Customers { get; set; }

        public BookingsDataStore()
        {
            Cars = CreateDummyCars();
            Customers = CreateDummyCustomers();
            UpdateCustomersWithIDs();
            UpdateCarsWithIDs();

            Bookings = CreateDummyBookings();
        }

        private void UpdateCustomersWithIDs()
        {
            Customers[0].Id = Guid.Parse("f1a89c6b-4a15-45b2-8a8e-bcf69e30bdf0");
            Customers[1].Id = Guid.Parse("2c7fead1-28fa-4e18-b108-1c6855f3958d");
            Customers[2].Id = Guid.Parse("674b1dc2-90a5-41e1-92f1-13b5b1e2cb2a");
        }

        private void UpdateCarsWithIDs()
        {
            Cars[0].Id = Guid.Parse("4b1f9e3a-73f1-4ef7-83a9-74e45e8d5db7");
            Cars[1].Id = Guid.Parse("1e2a6c43-b2c1-4ac2-9fc3-12cce2ed8c9b");
            Cars[2].Id = Guid.Parse("cf7d65ea-b4e8-45d3-8e20-2c5c4af6f503");
            Cars[3].Id = Guid.Parse("9f2e3a7a-c1b5-47dc-851e-1dbabf28a72d");
        }

        private List<Car> CreateDummyCars() 
        {
            return new List<Car>
            {
                new Car(CarModel.Bmw, 1998, "AB12345", CarCatagory.Combi),
                new Car(CarModel.Nissan, 2000, "BB877654",CarCatagory.Small),
                new Car(CarModel.Audi, 2024, "GB456789", CarCatagory.Truck),
                new Car(CarModel.Tesla, 2025, "TS567643", CarCatagory.Combi)
            };
        }

        private List<Customer> CreateDummyCustomers()
        {
            return new List<Customer>
            {
                new Customer("Ola Nordmann", "ola@gmail.com","95741658","14027424563"),
                new Customer("Kari Jensen", "Kari@yahoo.com","12345678","25118566558"),
                new Customer("Petter Pettersen", "petter@outlook.com", "45789654", "01018824785")
            };
        }

        private List<Booking> CreateDummyBookings()
        {
            return new List<Booking> {
                new Booking
                {
                    BookingDate = DateTime.UtcNow.AddDays(-5),
                    StartDate = DateTime.UtcNow.AddDays(1),
                    EndDate = DateTime.UtcNow.AddDays(7),
                    CarId = Guid.NewGuid(),
                    CarCatagory = CarCatagory.Small,
                    CarRegistrationNumber = "AB12345",
                    CustomerSSN = "14027424563",
                    CustomerId = Guid.NewGuid(),
                    RentalPrice = 1500m, //Random tall - ikke regnet utfra formelen foreløpig
                    NumberOfDays = 6,
                    NumberOfKm = 450
                },
                new Booking
                {
                    BookingDate = DateTime.UtcNow.AddDays(-3),
                    StartDate = DateTime.UtcNow.AddDays(5),
                    EndDate = DateTime.UtcNow.AddDays(12),
                    CarId = Guid.NewGuid(),
                    CarCatagory = CarCatagory.Truck,
                    CarRegistrationNumber = "KL54321",
                    CustomerId = Guid.NewGuid(),
                    CustomerSSN = "25118566558",
                    RentalPrice = 2500m, //Random tall - ikke regnet utfra formelen foreløpig
                    NumberOfDays = 7,
                    NumberOfKm = 800
                }

            };
        }

    }
}
