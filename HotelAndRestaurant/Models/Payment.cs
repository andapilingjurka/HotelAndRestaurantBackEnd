namespace HotelAndRestaurant.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }

        public string Amount { get; set; }
        public Guid BookingID { get; set; }

        public Booking Booking { get; set; }
    }
}
