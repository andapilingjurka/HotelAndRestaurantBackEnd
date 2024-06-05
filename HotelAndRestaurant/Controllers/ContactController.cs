using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var contact = await _db.Contact.ToListAsync();
            return Ok(contact);
        }

        //GetById
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetAeroplaniByIdAsync(int Id)
        {
            var contact = await _db.Contact.FindAsync(Id);
            return Ok(contact);
        }

        //Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(Contact contact)
        {
            _db.Contact.Add(contact);
            await _db.SaveChangesAsync();
            return Created($"/GetUserById/{contact.Id}", contact);
        }



        //Delete
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var contactDelete = await _db.Contact.FindAsync(Id);
            if (contactDelete == null)
            {
                return NotFound();
            }
            _db.Contact.Remove(contactDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        //Filtering
        [HttpGet]
        [Route("GetAllFiltering")]
        public async Task<IActionResult> GetFiltering([FromQuery] string searchQuery, [FromQuery] string sortField, [FromQuery] bool isAscending)
        {
            var query = _db.Contact.AsQueryable();
            int searchQueryInt;

            // Kërkimi
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(c =>
                    c.Name.Contains(searchQuery) ||
                    c.Email.Contains(searchQuery) ||
                    c.Message.Contains(searchQuery) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && c.Id == searchQueryInt) 
                );
            }

            // Renditja
            switch (sortField?.ToLower())
            {
                case "id":
                    query = isAscending ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id);
                    break;

                case "name":
                    query = isAscending ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
                    break;

                case "email":
                    query = isAscending ? query.OrderBy(c => c.Email) : query.OrderByDescending(c => c.Email);
                    break;

                case "message":
                    query = isAscending ? query.OrderBy(c => c.Message) : query.OrderByDescending(c => c.Message);
                    break;


                default:
                    query = query.OrderBy(c => c.Id);
                    break;
            }

            var result = await query.ToListAsync();
            return Ok(result);
        }
    }
}