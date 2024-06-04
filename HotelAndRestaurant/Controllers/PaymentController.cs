using HotelAndRestaurant.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelAndRestaurant.Models;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var payment = await _db.Payment.Include(q => q.Booking).ToListAsync();
            return Ok(payment);
        }

        //GetById
        [HttpGet]
        [Route("GetPaymentById")]
        public async Task<IActionResult> GetPagesaByIdAsync(int id)
        {
            var payment = await _db.Payment.Include(q => q.Booking).FirstOrDefaultAsync(q => q.Id == id);
            return Ok(payment);
        }

        //Add
        [HttpPost]
        [Route("AddPayment")]
        public async Task<IActionResult> PostAsync(Payment payment)
        {
            var existingBooking = await _db.Bookings.FindAsync(payment.BookingID);
            if (existingBooking == null)
            {
                return NotFound($"Booking me ID {payment.BookingID} nuk ekziston.");
            }

            payment.Booking = existingBooking;
            _db.Payment.Add(payment);
            await _db.SaveChangesAsync();
            return Created($"id/{payment.Id}", payment);
        }

        //Update
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutAsync(Payment payment)
        {
            var existingBooking = await _db.Bookings.FindAsync(payment.BookingID);
            if (existingBooking == null)
            {
                return NotFound($"Booking me ID {payment.BookingID} nuk ekziston.");
            }

            payment.Booking = existingBooking;
            _db.Payment.Update(payment);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        //Delete
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var paymentIdDelete = await _db.Payment.Include(q => q.Booking).FirstOrDefaultAsync(q => q.Id == id);

            if (paymentIdDelete == null)
            {
                return NotFound();
            }

            _db.Payment.Remove(paymentIdDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        //Filtering
        [HttpGet]
        [Route("GetAllFiltering")]
        public async Task<IActionResult> GetFiltering([FromQuery] string searchQuery, [FromQuery] string sortField, [FromQuery] bool isAscending)
        {
            var query = _db.Payment.AsQueryable();
            int searchQueryInt;
            DateTime searchQueryDate;

            // Kërkimi
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p =>
                    p.Name.Contains(searchQuery) ||
                    p.Surname.Contains(searchQuery) ||
                    p.Phone.Contains(searchQuery) ||
                    p.Amount.Contains(searchQuery) ||
                    (p.BookingID.ToString().Contains(searchQuery)) ||
                    (int.TryParse(searchQuery, out searchQueryInt) && p.Id == searchQueryInt) ||
                    (DateTime.TryParse(searchQuery, out searchQueryDate) && p.Date.Date == searchQueryDate.Date)
                );
            }

            // Renditja
            switch (sortField.ToLower())
            {
                case "id":
                    query = isAscending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                    break;

                case "name":
                    query = isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;

                case "surname":
                    query = isAscending ? query.OrderBy(p => p.Surname) : query.OrderByDescending(p => p.Surname);
                    break;

                case "phone":
                    query = isAscending ? query.OrderBy(p => p.Phone) : query.OrderByDescending(p => p.Phone);
                    break;

                case "date":
                    query = isAscending ? query.OrderBy(p => p.Date) : query.OrderByDescending(p => p.Date);
                    break;

                case "amount":
                    query = isAscending ? query.OrderBy(p => p.Amount) : query.OrderByDescending(p => p.Amount);
                    break;

                case "bookingid":
                    query = isAscending ? query.OrderBy(p => p.BookingID) : query.OrderByDescending(p => p.BookingID);
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