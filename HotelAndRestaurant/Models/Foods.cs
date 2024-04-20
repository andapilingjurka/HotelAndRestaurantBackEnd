using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HotelAndRestaurant.Models
{
    public class Foods
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

    }


}