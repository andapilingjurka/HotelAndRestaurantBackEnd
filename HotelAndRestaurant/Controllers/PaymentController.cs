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
    }
}