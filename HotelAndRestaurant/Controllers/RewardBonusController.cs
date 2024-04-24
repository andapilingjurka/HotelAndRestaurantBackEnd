using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardBonusController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public RewardBonusController(ApplicationDbContext db)
        {
            _db = db;
        }

        // ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var rewardBonuses = await _db.rewardBonus.ToListAsync();
            return Ok(rewardBonuses);
        }

        // GetById
        [HttpGet]
        [Route("GetRewardBonusById")]
        public async Task<IActionResult> GetRewardBonusByIdAsync(int id)
        {
            var rewardBonus = await _db.rewardBonus.FindAsync(id);
            return Ok(rewardBonus);
        }

        // Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(RewardBonus rewardBonus)
        {
            _db.rewardBonus.Add(rewardBonus);
            await _db.SaveChangesAsync();
            return Created($"/GetRewardBonusById/{rewardBonus.Id}", rewardBonus);
        }

        // Update
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutAsync(int id, RewardBonus rewardBonus)
        {
            if (id != rewardBonus.Id)
            {
                return BadRequest();
            }

            _db.Entry(rewardBonus).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // Delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var rewardBonusToDelete = await _db.rewardBonus.FindAsync(id);

            if (rewardBonusToDelete == null)
            {
                return NotFound();
            }

            _db.rewardBonus.Remove(rewardBonusToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
