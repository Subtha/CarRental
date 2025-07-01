using CarRental.Application.Models;
using CarRental.Application.Models.CreationDTOs;
using CarRental.Application.Services;
using CarRental.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace CarRental.Tests.Services
{
    public class BookingServiceTests
    {
        [Fact]
        public async Task CreateBooking_ValidateInput_ReturnSuccess()
        {
            // Arrange
            var bookingsDataStore = new BookingsDataStore();
            var logger = new Mock<ILogger<BookingService>>();
            var options = Options.Create( new BookingPriceCalculationData
            {
                BaseDayRental = 650m,
                BaseKmPrice = 3.50m,
                ExtraChargeCombi = 1.3m,
                ExtraChargeTruck = 1.5m
            });
            var bookingService = new BookingService(bookingsDataStore, logger.Object, options);
            var bookingCreationDTO = new BookingCreationDTO
            {
                CarId = Guid.Parse("9f2e3a7a-c1b5-47dc-851e-1dbabf28a72d"),
                CustomerId = Guid.Parse("2c7fead1-28fa-4e18-b108-1c6855f3958d"),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                NumberOfKm = 100
            };

            // Act
            var result = await bookingService.CreateBooking(bookingCreationDTO);
            
            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

    }
}
