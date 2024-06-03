using HotelAndRestaurant.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Payment> Payment { get; set; }



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

            modelBuilder.Entity<Notification>()
               .HasOne(p => p.Room)
               .WithMany(r => r.Notifications)
               .HasForeignKey(p => p.RoomId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
           .HasOne(p => p.Booking)
           .WithMany()
           .HasForeignKey(p => p.BookingID) //Foreign Key
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "admin", Description = "Administrator with full access to all features and settings." },
                new Role { Id = 2, Name = "client", Description = "Regular user with access to basic features." },
                new Role { Id = 3, Name = "receptionist", Description = "Receptionist with access to front desk functionalities." },
                new Role { Id = 4, Name = "housekeeper", Description = "Housekeeper with access to housekeeping-related features." }
            );
            modelBuilder.Entity<User>().HasData(
              new User { Id = 1, FirstName = "admin",LastName="", Email = "admin@hotel.com", Password = "$2a$10$rL9JAcahgl2uzhgRdDt9cuV0NNZNNGE9T4.H9SXsl72kM7Vws1BDm", RoleId = 1 },
              new User { Id = 2, FirstName = "client", LastName = "", Email = "user@hotel.com", Password = "$2a$10$rL9JAcahgl2uzhgRdDt9cuV0NNZNNGE9T4.H9SXsl72kM7Vws1BDm", RoleId = 2 },
              new User { Id = 3, FirstName = "receptionist", LastName = "", Email = "receptionist@hotel.com", Password = "$2a$10$rL9JAcahgl2uzhgRdDt9cuV0NNZNNGE9T4.H9SXsl72kM7Vws1BDm", RoleId = 3 },
              new User { Id = 4, FirstName = "housekeeper", LastName = "", Email = "housekeeper1@hotel.com", Password = "$2a$10$rL9JAcahgl2uzhgRdDt9cuV0NNZNNGE9T4.H9SXsl72kM7Vws1BDm", RoleId = 4 }
          );
        }

        }

    }

