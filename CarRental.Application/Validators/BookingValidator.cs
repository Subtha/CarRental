using CarRental.Application.Models.CreationDTOs;

namespace CarRental.Application.Validators
{
    public static class BookingValidator
    {
        public static ValidationResult Validate(BookingCreationDTO bookingCreationDTO)
        {
            var result = new ValidationResult();

            if (bookingCreationDTO == null)
            {
                result.ErrorMessages.Add("Booking data can not be null.");
                return result; //ingen data å prossesere, so return med engang.
            }

            if (bookingCreationDTO.StartDate >= bookingCreationDTO.EndDate)
            {
                result.ErrorMessages.Add("StartDate must be earlier than end-date");
            }

            if (bookingCreationDTO.CarId == Guid.Empty)
            {
                result.ErrorMessages.Add("Car ID can not be emtpy.");
            }

            if (bookingCreationDTO.CustomerId == Guid.Empty)
            {
                result.ErrorMessages.Add("Customer ID can not be empty.");
            }

            return result;
        }
    }
}
