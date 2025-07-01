namespace CarRental.Application.Models
{
    public class BookingPriceCalculationData
    {
        public decimal BaseDayRental { get; set; }
        public decimal BaseKmPrice { get; set; }
        public decimal ExtraChargeCombi { get; set; }
        public decimal ExtraChargeTruck { get; set; }
    }
}
