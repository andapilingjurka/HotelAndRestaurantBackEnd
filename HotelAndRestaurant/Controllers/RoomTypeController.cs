using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public RoomTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var roomtype = await _db.RoomType.ToListAsync();
            return Ok(roomtype);
        }

        //GetById
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetShtetiByIdAsync(int Id)
        {
            var roomtype = await _db.RoomType.FindAsync(Id);
            return Ok(roomtype);
        }

        //Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(RoomType roomtype)
        {
            _db.RoomType.Add(roomtype);
            await _db.SaveChangesAsync();
            return Created($"/GetUserById/{roomtype.Id}", roomtype);
        }

        //Update
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutAsync(RoomType roomtype)
        {
            _db.RoomType.Update(roomtype);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var roomtypeIdDelete = await _db.RoomType.FindAsync(Id);
            if (roomtypeIdDelete == null)
            {
                return NotFound();
            }
            _db.RoomType.Remove(roomtypeIdDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}
