namespace HotelAndRestaurant.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public Room Room { get; set; }
    }
}
