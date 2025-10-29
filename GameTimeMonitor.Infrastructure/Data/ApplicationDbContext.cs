using GameTimeMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<RemoteControlCommand> RemoteControlCommands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Role).IsRequired();

                // Self-referencing relationship for Parent-Child
                entity.HasOne(u => u.Parent)
                    .WithMany(u => u.Children)
                    .HasForeignKey(u => u.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Device configuration
            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeviceName).IsRequired();
                entity.Property(e => e.DeviceIdentifier).IsRequired();
                entity.HasIndex(e => e.DeviceIdentifier).IsUnique();
                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(u => u.Devices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Activity configuration
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.ApplicationName).IsRequired();

                entity.HasOne(a => a.Device)
                    .WithMany(d => d.Activities)
                    .HasForeignKey(a => a.DeviceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // RemoteControlCommand configuration
            modelBuilder.Entity<RemoteControlCommand>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IssuedAt).IsRequired();
                entity.Property(e => e.Executed).IsRequired();

                entity.HasOne(rc => rc.Device)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeviceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
