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
    }
}
