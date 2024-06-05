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

        public DbSet<Contact> Contact { get; set; }


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
            modelBuilder.Entity<RoomType>().HasData(
                  new RoomType { Id = 21, RoomName = "Single" },
                  new RoomType { Id = 22, RoomName = "Double" },
                  new RoomType { Id = 23, RoomName = "Suite" }
              );

            // Seed data for Room
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 21,
                    RoomNumber = 101,
                    Status = 0,
                    Image = "/images/image1.jpg",
                    Price = "100",
                    Description = "A cozy single room.",
                    NeedsCleaning = false,
                    RoomTypeId = 21 // Single
                },
                new Room
                {
                    Id = 22,
                    RoomNumber = 102,
                    Status = 0,


                    Image = "/images/image2.jpg",
                    Price = "150",
                    Description = "A spacious double room.",
                    NeedsCleaning = true,
                    RoomTypeId = 22 // Double
                },
                new Room
                {
                    Id = 23,
                    RoomNumber = 103,
                    Status = 0,

                    Image = "/images/image3.jpg",
                    Price = "200",
                    Description = "A luxurious suite.",
                    NeedsCleaning = false,
                    RoomTypeId = 23 // Suite
                },
                new Room
                {
                    Id = 24,
                    RoomNumber = 104,
                    Status = 0,

                    Image = "/images/image4.jpg",
                    Price = "120",
                    Description = "A comfortable twin room.",
                    NeedsCleaning = false,
                    RoomTypeId = 22 // Double
                },
                new Room
                {
                    Id = 25,
                    RoomNumber = 105,
                    Status = 0,

                    Image = "/images/image5.jpg",
                    Price = "180",
                    Description = "A deluxe double room.",
                    NeedsCleaning = true,
                    RoomTypeId = 22 // Double
                },
                new Room
                {
                    Id = 26,
                    RoomNumber = 106,
                    Status = 0,

                    Image = "/images/image6.jpg",
                    Price = "250",
                    Description = "A premium suite.",
                    NeedsCleaning = false,
                    RoomTypeId = 23
                }
            );
        }

        }

    }

