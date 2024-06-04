using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public StaffController(ApplicationDbContext db)
        {
            _db = db;
        }

        // ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var staffList = await _db.Stafi.ToListAsync();
            return Ok(staffList);
        }

        // GetById
        [HttpGet]
        [Route("GetStaffById")]
        public async Task<IActionResult> GetStaffByIdAsync(int stafiId)
        {
            var staff = await _db.Stafi.FirstOrDefaultAsync(s => s.stafiId == stafiId);
            return Ok(staff);
        }

        // Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(stafi staff)
        {
            _db.Stafi.Add(staff);
            await _db.SaveChangesAsync();
            return Created($"/GetStaffById/{staff.stafiId}", staff);
        }

        // Update
        [HttpPut]
        [Route("Update/{stafiId}")]
        public async Task<IActionResult> PutAsync(int stafiId, stafi staff)
        {
            if (stafiId != staff.stafiId)
            {
                return BadRequest();
            }

            _db.Entry(staff).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // Delete
        [HttpDelete]
        [Route("Delete/{stafiId}")]
        public async Task<IActionResult> DeleteAsync(int stafiId)
        {
            var staffToDelete = await _db.Stafi.FindAsync(stafiId);

            if (staffToDelete == null)
            {
                return NotFound();
            }

            _db.Stafi.Remove(staffToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        //Filtering
        [HttpGet]
        [Route("GetAllFiltering")]
        public async Task<IActionResult> GetFiltering([FromQuery] string searchQuery, [FromQuery] string sortField, [FromQuery] bool isAscending)
        {
            var query = _db.Stafi.AsQueryable(); 
            int searchQueryInt;

            // Kërkimi
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p =>
                    p.name.Contains(searchQuery) ||
                    p.surname.Contains(searchQuery) ||
                    p.Image.Contains(searchQuery) ||
                    p.nrPhone.Contains(searchQuery) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.id == searchQueryInt) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.stafiId == searchQueryInt) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.cualification == searchQueryInt) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.RewardBonusId == searchQueryInt)
                );
            }

            // Renditja
            switch (sortField?.ToLower())
            {
                case "id":
                    query = isAscending ? query.OrderBy(p => p.id) : query.OrderByDescending(p => p.id);
                    break;

                case "name":
                    query = isAscending ? query.OrderBy(p => p.name) : query.OrderByDescending(p => p.name);
                    break;

                case "surname":
                    query = isAscending ? query.OrderBy(p => p.surname) : query.OrderByDescending(p => p.surname);
                    break;

                case "image":
                    query = isAscending ? query.OrderBy(p => p.Image) : query.OrderByDescending(p => p.Image);
                    break;

                case "nrphone":
                    query = isAscending ? query.OrderBy(p => p.nrPhone) : query.OrderByDescending(p => p.nrPhone);
                    break;

                case "cualification":
                    query = isAscending ? query.OrderBy(p => p.cualification) : query.OrderByDescending(p => p.cualification);
                    break;

                case "rewardbonusid":
                    query = isAscending ? query.OrderBy(p => p.RewardBonusId) : query.OrderByDescending(p => p.RewardBonusId);
                    break;

                default:
                    query = query.OrderBy(p => p.id);
                    break;
            }

            var result = await query.ToListAsync();
            return Ok(result);
        }
    }
}
