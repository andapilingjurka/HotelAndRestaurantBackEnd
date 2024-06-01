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

        public DbSet<Room> Room { get; set; }

        public DbSet<RoomType> RoomType { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

         public DbSet<stafi> Stafi { get; set; }    

        public DbSet<RewardBonus> rewardBonus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
            .HasOne(p => p.RoomType)
            .WithMany()
            .HasForeignKey(p => p.RoomTypeId) //Foreign Key
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
          .HasOne(p => p.Room)
          .WithMany()
          .HasForeignKey(p => p.RoomId) 
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
          .HasOne(p => p.User)
          .WithMany()
          .HasForeignKey(p => p.UserId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
       .HasOne(p => p.Role)
       .WithMany()
       .HasForeignKey(p => p.RoleId) //Foreign Key
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<stafi>()
           .HasOne(p => p.RewardBonus)
           .WithMany()
           .HasForeignKey(p => p.RewardBonusId)
           .OnDelete(DeleteBehavior.Restrict);
        


    }

    }
}
