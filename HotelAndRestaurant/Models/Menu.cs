using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HotelAndRestaurant.Models
{
    public class Menu
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FoodID { get; set; }
    }
}