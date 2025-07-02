using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Application.Models;
using CarRental.Application.Models.CreationDTOs;
using CarRental.Application.Validators;
using CarRental.Domain.Enums;
using CarRental.Domain.Models;
using CarRental.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarRental.Application.Services
{
    public class BookingService : IBookingService
    {        
        private readonly BookingsDataStore _bookingsDataStore;
        private readonly BookingPriceCalculationData _bookingPriceCalculationData;
        private readonly ILogger<BookingService> _logger;

        public BookingService(BookingsDataStore bookingsDataStore, ILogger<BookingService> logger, IOptions<BookingPriceCalculationData> priceCalcData) 
        {
            _bookingsDataStore = bookingsDataStore ?? throw new ArgumentNullException(nameof(bookingsDataStore), "BookingsDataStore is null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger-instance is null");
            _bookingPriceCalculationData = priceCalcData.Value ?? throw new ArgumentNullException(nameof(priceCalcData), "BookingPriceCalculationData is null");

        }

        public async Task<OperationResult<BookingDTO>> GetBookingById(Guid id)
        {
            var booking = _bookingsDataStore.Bookings.FirstOrDefault(b => b.Id == id );
            if(booking == null)
            {
                return await Task.FromResult(new OperationResult<BookingDTO>
                {
                    Success = false,                   
                    Errors = new List<string> { string.Format("Booking witd ID: {0} not found.", id)}
                });
            }

            return await Task.FromResult(new OperationResult<BookingDTO>
            {
                Success = true,
                Data = new BookingDTO
                {
                    Id = booking.Id,
                    BookingDate = booking.BookingDate,
                    CarCatagory = booking.CarCatagory,
                    CarRegistrationNumber = booking.CarRegistrationNumber,
                    CarId = booking.CarId,
                    CustomerId = booking.CustomerId,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    RentalPrice = booking.RentalPrice,
                    NumberOfDays = booking.NumberOfDays,
                    NumberOfKm = booking.NumberOfKm
                }
            });
        }

        public async Task<List<BookingDTO>> GetAllBookings()
        {
            return await Task.FromResult(_bookingsDataStore.Bookings.Select(b => new BookingDTO
            {
                Id = b.Id,
                BookingDate = b.BookingDate,
                CarCatagory = b.CarCatagory,
                CarRegistrationNumber = b.CarRegistrationNumber,
                CarId = b.CarId,
                CustomerId = b.CustomerId,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                RentalPrice = b.RentalPrice,
                NumberOfDays = b.NumberOfDays,
                NumberOfKm = b.NumberOfKm
            }).ToList());
        }

        public Task<OperationResult<BookingDTO>> CreateBooking(BookingCreationDTO bookingCreationDTO)
        {
            var bookingValidateResult = BookingValidator.Validate(bookingCreationDTO);
            if (!bookingValidateResult.IsValid)
            {
                return Task.FromResult(new OperationResult<BookingDTO>
                {
                    Success = false,
                    Errors = bookingValidateResult.ErrorMessages
                });
            }

            //Sjekker om kunden eksistereer i systemet og vi får tak i SSN basert på cust.id. 
            var bookingCustomer = _bookingsDataStore.Customers.FirstOrDefault(cust => cust.Id == bookingCreationDTO.CustomerId);
            if (bookingCustomer == null)
            {
                return Task.FromResult(new OperationResult<BookingDTO>
                {
                    Success = false,
                    Errors = new List<string> { "Cutomer not found" }
                });
            }

            //Sjekker om bilen eksisterer og er ledig.
            var bookingCar = _bookingsDataStore.Cars.FirstOrDefault(car => car.Id == bookingCreationDTO.CarId);
            if (bookingCar == null || !bookingCar.IsAvailable)
            {
                return Task.FromResult(new OperationResult<BookingDTO>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Car with Id {0} not found or not available.", bookingCreationDTO.CarId) }                    
                });
                
            }

            //Går videre med å oprrette bookingen.
            //Deretter oppdaterer bilens tilgjengeligighet i CarsStore.

            //beregner calculated values basert på de dataene vi har
            var numberofDays = (bookingCreationDTO.EndDate - bookingCreationDTO.StartDate).Days;
            var rentalPrice = GetCalculatedRentalPrice(bookingCar.CarCatagory, numberofDays, bookingCreationDTO.NumberOfKm);

            var bookingToAdd = new Booking()
            {
                CarId = bookingCar.Id,
                CarCatagory = bookingCar.CarCatagory,
                CarRegistrationNumber = bookingCar.RegistrationNumber,
                CustomerId = bookingCustomer.Id,
                CustomerSSN = bookingCustomer.SSN,
                EndDate = bookingCreationDTO.EndDate,
                StartDate = bookingCreationDTO.StartDate,
                NumberOfDays = numberofDays,
                NumberOfKm = bookingCreationDTO.NumberOfKm,
                RentalPrice = rentalPrice
            };

            //lagre til db-store
            _bookingsDataStore.Bookings.Add(bookingToAdd);
            bookingCar.IsAvailable = false; //Oppdaterer tilgjengeligheten nå som bookingen er utført.
            _logger.LogInformation("Booking created: {@bookingToAdd} ", bookingToAdd );

            return Task.FromResult(new OperationResult<BookingDTO>
            {
                Success = true,
                Data = new BookingDTO
                {
                    Id = bookingToAdd.Id,
                    BookingDate = bookingToAdd.BookingDate,
                    CarId = bookingToAdd.CarId,
                    CarCatagory = bookingToAdd.CarCatagory,
                    CarRegistrationNumber = bookingToAdd.CarRegistrationNumber,
                    StartDate = bookingToAdd.StartDate,
                    EndDate = bookingToAdd.EndDate,
                    RentalPrice = bookingToAdd.RentalPrice,
                    CustomerId = bookingToAdd.Id,
                    NumberOfDays = bookingToAdd.NumberOfDays,
                    NumberOfKm = bookingToAdd.NumberOfKm
                },
            });
        }

        private decimal GetCalculatedRentalPrice(CarCatagory carCatagory, int numberOfDays, int numberOfKm)
        {
            decimal rentalPrice = 0m;
            if (carCatagory == CarCatagory.Small)
            {
                rentalPrice = _bookingPriceCalculationData.BaseDayRental * numberOfDays;

                return rentalPrice;
            }
            else if (carCatagory == CarCatagory.Combi) 
            {
                rentalPrice = (_bookingPriceCalculationData.BaseDayRental * numberOfDays * _bookingPriceCalculationData.ExtraChargeCombi) + 
                    (_bookingPriceCalculationData.BaseKmPrice * numberOfKm); 

                return rentalPrice;
            }
            else if (carCatagory == CarCatagory.Truck)
            {
                rentalPrice = (_bookingPriceCalculationData.BaseDayRental * numberOfDays * _bookingPriceCalculationData.ExtraChargeTruck) + 
                    (_bookingPriceCalculationData.BaseKmPrice * numberOfKm * _bookingPriceCalculationData.ExtraChargeTruck);

                return rentalPrice;
            }

            return rentalPrice;
        }

    }
}
