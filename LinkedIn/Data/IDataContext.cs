using LinkedIn.Models.Messages;
using LinkedIn.Models.Pages;
using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Models.ProfileDetails.Skills;
using LinkedIn.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LinkedIn.Data
{
    public interface IDataContext : IDisposable
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PendingConnections> PendingConnections { get; set; }
        public DbSet<ProfileDetails> ProfileDetails { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<UserEducation> UserEducations { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<LicensesAndCertifications> LicensesAndCertifications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }
        public DbSet<CompanyLocation> CompanyLocations { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<Message> Messages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void LogTracker();
    }
}
