using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IMongoCollection<Foods> _foods;

        public FoodsController(IMongoClient client)
        {
            var database = client.GetDatabase("Restaurant");
            _foods = database.GetCollection<Foods>("foods");
        }

        //Get
        [HttpGet]
        public ActionResult<List<Foods>> Get() =>
           _foods.Find(menu => true).ToList();

        [HttpGet("{id:length(24)}", Name = "GetFoods")]
        public ActionResult<Foods> Get(string id)
        {
            var foods = _foods.Find<Foods>(o => o.Id == id).FirstOrDefault();

            if (foods == null)
            {
                return NotFound();
            }

            return foods;
        }

        //Post
        [HttpPost]
        public ActionResult<Foods> Create(Foods ushqimi)
        {
            try
            {
                // Generate a unique ID
                ushqimi.Id = ObjectId.GenerateNewId().ToString(); // Generate a unique ObjectId and convert it to string
                _foods.InsertOne(ushqimi);
                return CreatedAtRoute("GetFoods", new { id = ushqimi.Id }, ushqimi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Put
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Foods updatedUshqimi)
        {
            try
            {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    return BadRequest("ID nuk është në formatin e duhur.");
                }

                var filter = Builders<Foods>.Filter.Eq("_id", objectId);
                var ushqimi = _foods.Find(filter).FirstOrDefault();

                if (ushqimi == null)
                {
                    return NotFound();
                }

                _foods.ReplaceOne(filter, updatedUshqimi);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Delete
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    return BadRequest("ID nuk është në formatin e duhur.");
                }

                var filter = Builders<Foods>.Filter.Eq("_id", objectId);
                var ushqimi = _foods.Find(filter).FirstOrDefault();

                if (ushqimi == null)
                {
                    return NotFound();
                }

                _foods.DeleteOne(filter);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}