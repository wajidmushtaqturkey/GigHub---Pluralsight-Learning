using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Core.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId });

            builder.Entity<Attendance>()
                .HasOne(a => a.Gig)
                .WithMany(g => g.Attendances)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);


            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Followee)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithOne(f => f.Follower)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<UserNotification>()
                .HasKey(u => new { u.NotificationId, u.UserId });

            builder.Entity<UserNotification>()
                .HasOne(n => n.User)
                .WithMany(u => u.UserNotifications)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Following>()
                .HasKey(f => new { f.FolloweeId, f.FollowerId });

            base.OnModelCreating(builder);
        }
    }
}