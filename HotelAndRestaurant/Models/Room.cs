using System.ComponentModel.DataAnnotations;

namespace HotelAndRestaurant.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public int RoomNumber { get; set; }

    
        public string Status { get; set; }

        public string Image { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public int RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }
    }
}
