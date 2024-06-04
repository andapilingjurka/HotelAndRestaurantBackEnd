using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models;
using HotelAndRestaurant.Models.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HotelAndRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("send-notification")]
        public async Task<ActionResult<Notification>> CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send real-time notification
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message);

            return Ok(notification);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Notification>> GetNotifications()
        {
            var notifications = _context.Notifications.Where(n => !n.IsRead).ToList();
            return Ok(notifications);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            var notification = _context.Notifications.Find(id);
            if (notification == null)
            {
                return NotFound();
            }
            notification.IsRead = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
