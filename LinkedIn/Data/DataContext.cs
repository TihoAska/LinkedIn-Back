using LinkedIn.Models.Pages;
using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Models.ProfileDetails.Skills;
using LinkedIn.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Data
{
    public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PendingConnections> PendingConnections { get; set; }
        public DbSet<ProfileDetails> ProfileDetails { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<UserEducation> UserEducations { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<LicensesAndCertifications> LicensesAndCertifications { get; set; }
        public DbSet<UserSkills> Skills { get; set; }
        public DbSet<CompanyLocation> CompanyLocations { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PendingConnections>(entity =>
            {
                entity.HasKey(pc => pc.Id);
                entity.HasOne(pc => pc.Sender)
                    .WithMany(u => u.SentConnections)
                    .HasForeignKey(pc => pc.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pc => pc.Receiver)
                    .WithMany(u => u.ReceivedConnections)
                    .HasForeignKey(pc => pc.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Page>()
            .HasMany(p => p.Followers)
            .WithMany(u => u.PagesFollowing)
            .UsingEntity<Dictionary<string, object>>(
                "PageUser",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("FollowersId")
                    .HasConstraintName("FK_PageUser_FollowersId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Page>()
                    .WithMany()
                    .HasForeignKey("PagesFollowingId")
                    .HasConstraintName("FK_PageUser_PagesFollowingId")
                    .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
