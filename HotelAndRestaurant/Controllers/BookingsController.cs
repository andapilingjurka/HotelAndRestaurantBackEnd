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
            var booking = await _db.Bookings.Include(q => q.Guest).Include(q => q.Room).ToListAsync();
            return Ok(booking);
        }

        //GetById
        [HttpGet]
        [Route("GetBookingById")]
        public async Task<IActionResult> GetBookingByIdAsync(Guid Id)
        {
            var booking = await _db.Bookings.Include(q => q.Guest).Include(q=>q.Room).FirstOrDefaultAsync(q => q.Id == Id);
            return Ok(booking);
        }
        //Get by guest name
        [HttpGet]
        [Route("GetBookingsByGuestName")]
        public async Task<IActionResult> GetBookingsByGuestNameAsync(string guestName, string guestLastName)
        {
            var bookings = await _db.Bookings
                .Include(q => q.Guest)
                .Include(q => q.Room)
                .Where(q => q.Guest.Name == guestName && q.Guest.LastName == guestLastName)
                .ToListAsync();

            return Ok(bookings);
        }
        //Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(Booking booking)
        {
            
            var existingGuest = await _db.Guests.FindAsync(booking.GuestId);
            var existingRoom = await _db.Room.FindAsync(booking.RoomId);
            if (existingGuest == null)
            {
                return NotFound($"Guest me ID {booking.GuestId} nuk ekziston.");
            }
            if (existingRoom == null)
            {
                return NotFound($"Room me ID {booking.RoomId} nuk ekziston.");
            }

            booking.Guest = existingGuest;
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
            var existingGuest = await _db.Guests.FindAsync(booking.GuestId);
            var existingRoom = await _db.Room.FindAsync(booking.RoomId);
            if (existingGuest == null)
            {
                return NotFound($"Guest me ID {booking.GuestId} nuk ekziston.");
            }
            if (existingRoom == null)
            {
                return NotFound($"Room me ID {booking.RoomId} nuk ekziston.");
            }
            booking.Guest = existingGuest;
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
            var bookingIdToDelete= await _db.Bookings.Include(q => q.Guest).Include(q=>q.Room).FirstOrDefaultAsync(q => q.Id == Id);

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



    }
}
