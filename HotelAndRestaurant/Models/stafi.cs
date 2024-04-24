using System.ComponentModel.DataAnnotations;

namespace HotelAndRestaurant.Models
{
    public class stafi
    {
        [Key]
        public int stafiId { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string Image { get; set; }

        public string nrPhone{ get; set; }

        public int cualification { get; set; }
        public int RewardBonusId { get; set; }

        public RewardBonus RewardBonus { get; set; }
    }


}

