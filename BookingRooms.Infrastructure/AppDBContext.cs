using BookingRooms.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRooms.Infrastructure
{
    public class AppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FullName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.Role)
                      .HasConversion<string>()
                      .IsRequired();
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Number)
                      .IsRequired();
                entity.Property(r => r.Capacity)
                      .IsRequired();
                entity.Property(r => r.PricePerNight)
                      .HasColumnType("decimal(18,2)");
                entity.Property(u => u.State)
                      .HasConversion<string>()
                      .IsRequired();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.HasOne(r => r.Room)
                      .WithMany()
                      .HasForeignKey(b => b.RoomId);
                entity.HasOne(r => r.User)
                       .WithMany()
                       .HasForeignKey(b => b.UserId);
                entity.Property(r => r.StartDate);
                entity.Property(r => r.EndDate);
                entity.Property(u => u.State)
                      .HasConversion<string>()
                      .IsRequired();
            });
        }
    }
}
