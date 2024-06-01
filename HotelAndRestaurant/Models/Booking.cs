using System.ComponentModel.DataAnnotations;

namespace HotelAndRestaurant.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public double ToTal { get; set; }

        public DateTimeOffset CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

      /*=====Foreign Keys=====*/
        public int? RoomId { get; set; }
        public int? UserId { get; set; }

        public Room Room { get; set; }

        public User User { get; set; }

    }
}
