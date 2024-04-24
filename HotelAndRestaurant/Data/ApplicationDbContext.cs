using HotelAndRestaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAndRestaurant.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public DbSet<RoomType> RoomType { get; set; }
    }
}
