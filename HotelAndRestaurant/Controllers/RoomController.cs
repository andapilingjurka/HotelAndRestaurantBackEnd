using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public RoomController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var room = await _db.Room.Include(q => q.RoomType).ToListAsync();
            return Ok(room);
        }

        //GetById
        [HttpGet]
        [Route("GetRoomById")]
        public async Task<IActionResult> GetRoomByIdAsync(int Id)
        {
            var room = await _db.Room.Include(q => q.RoomType).FirstOrDefaultAsync(q => q.Id == Id);
            return Ok(room);
        }

        //Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(Room room)
        {
            // Kontrollo nëse roomtype ekziston
            var existingRoomType = await _db.RoomType.FindAsync(room.RoomTypeId);
            if (existingRoomType == null)
            {
                return NotFound($"RoomType me ID {room.RoomTypeId} nuk ekziston.");
            }

            room.RoomType = existingRoomType;

            _db.Room.Add(room);
            await _db.SaveChangesAsync();
            return Created($"/GetUserById/{room.Id}", room);
        }

        //Update
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutAsync(Room room)
        {
            // Kontrollo nëse RoomType ekziston
            var existingRoomType = await _db.RoomType.FindAsync(room.RoomTypeId);
            if (existingRoomType == null)
            {
                return NotFound($"RoomType me ID {room.RoomTypeId} nuk ekziston.");
            }

            room.RoomType = existingRoomType;

            _db.Room.Update(room);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var roomIdDelete = await _db.Room.Include(q => q.RoomType).FirstOrDefaultAsync(q => q.Id == Id);

            if (roomIdDelete == null)
            {
                return NotFound();
            }

            _db.Room.Remove(roomIdDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        // Metoda për të gjetur dhomat bazuar në emrin e dhomës (RoomName)
        [HttpGet]
        [Route("GetRoomsByRoomType")]
        public async Task<IActionResult> GetRoomsByRoomTypeAsync(string roomTypeName)
        {
            var rooms = await _db.Room
                .Include(q => q.RoomType)
                .Where(q => q.RoomType.RoomName == roomTypeName)
                .ToListAsync();

            return Ok(rooms);
        }

        //Filtering
        [HttpGet]
        [Route("GetAllFiltering")]
        public async Task<IActionResult> GetFiltering([FromQuery] string searchQuery, [FromQuery] string sortField, [FromQuery] bool isAscending)
        {
            var query = _db.Room.AsQueryable(); 
            int searchQueryInt;

            // Kërkimi
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p =>
                    p.Status.Contains(searchQuery) ||
                    p.Image.Contains(searchQuery) ||
                    p.Price.Contains(searchQuery) ||
                    p.Description.Contains(searchQuery) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.Id == searchQueryInt) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.RoomNumber == searchQueryInt) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.RoomTypeId == searchQueryInt)
                );
            }

            // Renditja
            switch (sortField?.ToLower())
            {
                case "id":
                    query = isAscending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                    break;

                case "roomnumber":
                    query = isAscending ? query.OrderBy(p => p.RoomNumber) : query.OrderByDescending(p => p.RoomNumber);
                    break;

                case "status":
                    query = isAscending ? query.OrderBy(p => p.Status) : query.OrderByDescending(p => p.Status);
                    break;

                case "image":
                    query = isAscending ? query.OrderBy(p => p.Image) : query.OrderByDescending(p => p.Image);
                    break;

                case "price":
                    query = isAscending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                    break;

                case "description":
                    query = isAscending ? query.OrderBy(p => p.Description) : query.OrderByDescending(p => p.Description);
                    break;

                case "roomtypeid":
                    query = isAscending ? query.OrderBy(p => p.RoomTypeId) : query.OrderByDescending(p => p.RoomTypeId);
                    break;

                default:
                    query = query.OrderBy(p => p.Id);
                    break;
            }

            var result = await query.ToListAsync();
            return Ok(result);
        }
    }
}
