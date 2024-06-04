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

        //Filtering
        [HttpGet]
        [Route("GetAllFiltering")]
        public async Task<IActionResult> GetFiltering([FromQuery] string searchQuery, [FromQuery] string sortField, [FromQuery] bool isAscending)
        {
            var query = _db.rewardBonus.AsQueryable(); 
            int searchQueryInt;
            decimal searchQueryDecimal;
            DateTime searchQueryDate;

            // Kërkimi
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p =>
                    p.Name.Contains(searchQuery) ||
                    p.Reason.Contains(searchQuery) ||
                    (decimal.TryParse(searchQuery, out searchQueryDecimal) && p.Amount == searchQueryDecimal) ||
                    (DateTime.TryParse(searchQuery, out searchQueryDate) && p.Date.Date == searchQueryDate.Date) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.Id == searchQueryInt)
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

                case "amount":
                    query = isAscending ? query.OrderBy(p => p.Amount) : query.OrderByDescending(p => p.Amount);
                    break;

                case "reason":
                    query = isAscending ? query.OrderBy(p => p.Reason) : query.OrderByDescending(p => p.Reason);
                    break;

                case "date":
                    query = isAscending ? query.OrderBy(p => p.Date) : query.OrderByDescending(p => p.Date);
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
