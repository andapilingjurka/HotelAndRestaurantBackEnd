using System.ComponentModel.DataAnnotations;

namespace HotelAndRestaurant.Models
{
    public class Guest
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }


    }
}
