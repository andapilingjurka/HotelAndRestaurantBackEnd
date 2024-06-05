﻿// <auto-generated />
using System;
using HotelAndRestaurant.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelAndRestaurant.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240604214719_RefreshMigration")]
    partial class RefreshMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HotelAndRestaurant.Models.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CheckInDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<double>("ToTal")
                        .HasColumnType("float");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Guest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BookingID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingID");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.RewardBonus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("rewardBonus");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Administrator with full access to all features and settings.",
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Regular user with access to basic features.",
                            Name = "client"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Receptionist with access to front desk functionalities.",
                            Name = "receptionist"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Housekeeper with access to housekeeping-related features.",
                            Name = "housekeeper"
                        });
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NeedsCleaning")
                        .HasColumnType("bit");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoomType");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.stafi", b =>
                {
                    b.Property<int>("stafiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("stafiId"), 1L, 1);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RewardBonusId")
                        .HasColumnType("int");

                    b.Property<int>("cualification")
                        .HasColumnType("int");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nrPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("stafiId");

                    b.HasIndex("RewardBonusId");

                    b.ToTable("Stafi");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@hotel.com",
                            FirstName = "admin",
                            LastName = "",
                            Password = "$2a$10$rL9JAcahgl2uzhgRdDt9cuV0NNZNNGE9T4.H9SXsl72kM7Vws1BDm",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "user@hotel.com",
                            FirstName = "client",
                            LastName = "",
                            Password = "$2a$10$rL9JAcahgl2uzhgRdDt9cuV0NNZNNGE9T4.H9SXsl72kM7Vws1BDm",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            Email = "receptionist@hotel.com",
                            FirstName = "receptionist",
                            LastName = "",
                            Password = "$2a$10$rL9JAcahgl2uzhgRdDt9cuV0NNZNNGE9T4.H9SXsl72kM7Vws1BDm",
                            RoleId = 3
                        },
                        new
                        {
                            Id = 4,
                            Email = "housekeeper1@hotel.com",
                            FirstName = "housekeeper",
                            LastName = "",
                            Password = "$2a$10$rL9JAcahgl2uzhgRdDt9cuV0NNZNNGE9T4.H9SXsl72kM7Vws1BDm",
                            RoleId = 4
                        });
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Booking", b =>
                {
                    b.HasOne("HotelAndRestaurant.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HotelAndRestaurant.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Notification", b =>
                {
                    b.HasOne("HotelAndRestaurant.Models.Room", "Room")
                        .WithMany("Notifications")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Payment", b =>
                {
                    b.HasOne("HotelAndRestaurant.Models.Booking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Room", b =>
                {
                    b.HasOne("HotelAndRestaurant.Models.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.stafi", b =>
                {
                    b.HasOne("HotelAndRestaurant.Models.RewardBonus", "RewardBonus")
                        .WithMany()
                        .HasForeignKey("RewardBonusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RewardBonus");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.User", b =>
                {
                    b.HasOne("HotelAndRestaurant.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HotelAndRestaurant.Models.Room", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
