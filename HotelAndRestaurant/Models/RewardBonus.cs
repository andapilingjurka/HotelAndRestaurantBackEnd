using System;
using System.ComponentModel.DataAnnotations;

namespace HotelAndRestaurant.Models
{
    public class RewardBonus
    {
        [Key]
        public int Id { get; set; }      
        public string Name { get; set; }       
        public decimal Amount { get; set; }        
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}
