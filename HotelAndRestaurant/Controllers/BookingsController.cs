using HotelAndRestaurant.Data;
using HotelAndRestaurant.Migrations;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BookingsController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var booking = await _db.Bookings.Include(q => q.User).Include(q => q.Room).ToListAsync();
            return Ok(booking);
        }

        //GetById
        [HttpGet]
        [Route("GetBookingById")]
        public async Task<IActionResult> GetBookingByIdAsync(Guid Id)
        {
            var booking = await _db.Bookings.Include(q => q.User).Include(q => q.Room).FirstOrDefaultAsync(q => q.Id == Id);
            return Ok(booking);
        }

        //Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(Booking booking)
        {

            var existingGuest = await _db.Users.FindAsync(booking.UserId);
            var existingRoom = await _db.Room.FindAsync(booking.RoomId);
            if (existingGuest == null)
            {
                return NotFound($"Guest me ID {booking.UserId} nuk ekziston.");
            }
            if (existingRoom == null)
            {
                return NotFound($"Room me ID {booking.RoomId} nuk ekziston.");
            }

            booking.User = existingGuest;
            booking.Room = existingRoom;

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();
            return Created($"/GetUserById/{booking.Id}", booking);
        }

        //Update
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutAsync(Booking booking)
        {
            // Kontrollo nëse RoomType ekziston
            var existingGuest = await _db.Users.FindAsync(booking.UserId);
            var existingRoom = await _db.Room.FindAsync(booking.RoomId);
            if (existingGuest == null)
            {
                return NotFound($"Guest me ID {booking.UserId} nuk ekziston.");
            }
            if (existingRoom == null)
            {
                return NotFound($"Room me ID {booking.RoomId} nuk ekziston.");
            }
            booking.User = existingGuest;
            booking.Room = existingRoom;

            _db.Bookings.Update(booking);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var bookingIdToDelete = await _db.Bookings.Include(q => q.User).Include(q => q.Room).FirstOrDefaultAsync(q => q.Id == Id);

            if (bookingIdToDelete == null)
            {
                return NotFound();
            }

            _db.Bookings.Remove(bookingIdToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("BookedDates/{roomId}")]
        public async Task<IActionResult> GetBookedDates(int roomId)
        {
            var bookedDates = await _db.Bookings
                .Where(b => b.RoomId == roomId)
                .Select(b => new
                {
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate
                })
                .ToListAsync();

            return Ok(bookedDates);
        }
        [HttpGet]
        [Route("GetAllFiltering")]
        public async Task<IActionResult> GetFiltering([FromQuery] string searchQuery, [FromQuery] string sortField, [FromQuery] bool isAscending)
        {
            var query = _db.Bookings.AsQueryable();
            Guid searchQueryGuid;
            double searchQueryDouble;
            DateTimeOffset searchQueryDateOffset;
            DateTime searchQueryDate;
            int searchQueryInt;

            // Kërkimi
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p =>
                    p.Name.Contains(searchQuery) ||
                    p.LastName.Contains(searchQuery) ||
                    (p.ToTal.ToString().Contains(searchQuery)) ||
                    (Guid.TryParse(searchQuery, out searchQueryGuid) && p.Id == searchQueryGuid) ||
                    (DateTimeOffset.TryParse(searchQuery, out searchQueryDateOffset) && p.CheckInDate.Date == searchQueryDateOffset.Date) ||
                    (DateTime.TryParse(searchQuery, out searchQueryDate) && p.CheckOutDate.HasValue && p.CheckOutDate.Value.Date == searchQueryDate.Date) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.RoomId == searchQueryInt) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.UserId == searchQueryInt)
                );
            }

            // Renditja
            switch (sortField?.ToLower())
            {
                case "id":
                    query = isAscending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                    break;

                case "name":
                    query = isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;

                case "lastname":
                    query = isAscending ? query.OrderBy(p => p.LastName) : query.OrderByDescending(p => p.LastName);
                    break;

                case "total":
                    query = isAscending ? query.OrderBy(p => p.ToTal) : query.OrderByDescending(p => p.ToTal);
                    break;

                case "checkindate":
                    query = isAscending ? query.OrderBy(p => p.CheckInDate) : query.OrderByDescending(p => p.CheckInDate);
                    break;

                case "checkoutdate":
                    query = isAscending ? query.OrderBy(p => p.CheckOutDate) : query.OrderByDescending(p => p.CheckOutDate);
                    break;

                case "roomid":
                    query = isAscending ? query.OrderBy(p => p.RoomId) : query.OrderByDescending(p => p.RoomId);
                    break;

                case "userid":
                    query = isAscending ? query.OrderBy(p => p.UserId) : query.OrderByDescending(p => p.UserId);
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