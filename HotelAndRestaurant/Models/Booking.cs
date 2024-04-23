using System.ComponentModel.DataAnnotations;

namespace HotelAndRestaurant.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        public long Price { get; set; }
        public string Currency { get; set; }

        public DateTimeOffset CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

      /*=====Foreign Keys=====*/
        public int? RoomId { get; set; }
        public Guid? GuestId { get; set; }

        public Room Room { get; set; }

        public Guest Guest { get; set; }

    }
}
