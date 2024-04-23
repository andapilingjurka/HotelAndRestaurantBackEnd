using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
    

            private readonly ApplicationDbContext _db;

            public GuestsController(ApplicationDbContext db)
            {
                _db = db;
            }

            //ListAll
            [HttpGet]
            [Route("GetAllList")]
            public async Task<IActionResult> GetAsync()
            {
                var guests = await _db.Guests.ToListAsync();
                return Ok(guests);
            }

            //GetById
            [HttpGet]
            [Route("GetGuestById")]
            public async Task<IActionResult> GetGuestByIdAsync(Guid Id)
            {
                var guest = await _db.Guests.FindAsync(Id);
                return Ok(guest);
            }

            //Add
            [HttpPost]
            [Route("Add")]
            public async Task<IActionResult> PostAsync(Guest guest)
            {
                _db.Guests.Add(guest);
                await _db.SaveChangesAsync();
                return Created($"/GetUserById/{guest.Id}", guest);
            }

            //Update
            [HttpPut]
            [Route("Update/{id}")]
            public async Task<IActionResult> PutAsync(Guest guest)
            {
                _db.Guests.Update(guest);
                await _db.SaveChangesAsync();
                return NoContent();
            }

            //Delete
            [Route("Delete")]
            [HttpDelete]
            public async Task<IActionResult> DeleteAsync(Guid Id)
            {
                var guestIdToDelete = await _db.Guests.FindAsync(Id);
                if (guestIdToDelete == null)
                {
                    return NotFound();
                }
                _db.Guests.Remove(guestIdToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }

        }
    }


