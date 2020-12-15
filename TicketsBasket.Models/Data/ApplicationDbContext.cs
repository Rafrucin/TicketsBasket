using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketsBasket.Models.Domain;

namespace TicketsBasket.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<EventTag> EventTags { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WishListEvent> WishlistEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().HasMany(k => k.Events).WithOne(k => k.UserProfile).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>().HasMany(k => k.WishlistEvents).WithOne(k => k.UserProfile).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>().HasMany(k => k.Likes).WithOne(k => k.UserProfile).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>().HasMany(k => k.Tickets).WithOne(k => k.UserProfile).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>().HasMany(k => k.ReceivedApplications).WithOne(k => k.Organizer).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>().HasMany(k => k.SentApplications).WithOne(k => k.AppliedUser).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>().HasMany(k => k.EventTags).WithOne(k => k.Event).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>().HasMany(k => k.EventImages).WithOne(k => k.Event).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>().HasMany(k => k.Likes).WithOne(k => k.Event).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>().HasMany(k => k.Tickets).WithOne(k => k.Event).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>().HasMany(k => k.WishlistEvents).WithOne(k => k.Event).OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

    }
}
