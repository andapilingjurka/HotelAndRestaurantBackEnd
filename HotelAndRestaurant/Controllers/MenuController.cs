using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMongoCollection<Menu> _menu;
        private readonly IMongoCollection<Foods> _foods;

        public MenuController(IMongoClient client)
        {
            var database = client.GetDatabase("Restaurant");
            _menu = database.GetCollection<Menu>("menu");
            _foods = database.GetCollection<Foods>("foods");
        }

        //Get
        [HttpGet]
        public ActionResult<List<Menu>> Get() =>
            _menu.Find(menu => true).ToList();

        [HttpGet("{id:length(24)}", Name = "GetMenu")]
        public ActionResult<Menu> Get(string id)
        {
            var menu = _menu.Find<Menu>(o => o.Id == id).FirstOrDefault();

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        //Post
        [HttpPost]
        public ActionResult<Menu> Create(Menu menu)
        {
            // Kontrolloni nëse UshqimiId është në formatin e duhur për një ObjectId
            if (!ObjectId.TryParse(menu.FoodID.ToString(), out _))
            {
                return BadRequest($"Food ID '{menu.FoodID}'  is not in the correct format.");
            }

            var ushqimi = _foods.Find<Foods>(u => u.Id == menu.FoodID).FirstOrDefault();
            if (ushqimi == null)
            {
                return BadRequest($"The food with ID'{menu.FoodID}' does not exist in the database.");
            }

            menu.Id = ObjectId.GenerateNewId().ToString(); // Generate a unique ObjectId and convert it to string
            _menu.InsertOne(menu);
            return CreatedAtRoute("GetMenu", new { id = menu.Id }, menu);
        }

        //Put
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Menu updatedMenu)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("ID is not in the correct format.");
            }

            var filter = Builders<Menu>.Filter.Eq("_id", objectId);
            var menu = _menu.Find(filter).FirstOrDefault();

            if (menu == null)
            {
                return NotFound();
            }

            // Update the existing menu with the data from updatedmenu

            menu.FoodID = updatedMenu.FoodID; // Assuming you want to update UshqimiId as well

            _menu.ReplaceOne(filter, menu);
            return NoContent();
        }

        //Delete
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("ID is not in the correct format.");
            }

            var filter = Builders<Menu>.Filter.Eq("_id", objectId);
            var menu = _menu.Find(filter).FirstOrDefault();

            if (menu == null)
            {
                return NotFound();
            }

            _menu.DeleteOne(filter);
            return NoContent();
        }
    }
}