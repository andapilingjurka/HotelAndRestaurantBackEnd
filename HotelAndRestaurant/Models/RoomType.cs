using System.ComponentModel.DataAnnotations;

namespace HotelAndRestaurant.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        public string RoomName { get; set; }
    }
}
