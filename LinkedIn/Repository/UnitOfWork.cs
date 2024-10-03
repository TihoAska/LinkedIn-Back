using LinkedIn.Data;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LinkedIn.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext _context;
        public IPageRepository Pages { get; private set; }
        public IUserRepository Users { get; private set; }
        public IConnectionsRepository Connections { get; private set; }
        public IExperienceRepository Experiences { get; private set; }
        public IEducationRepository Educations { get; private set; }
        public IInstitutionRepository Institutions { get; private set; }
        public ILicensesRepository Licenses { get; private set; }
        public ICompanyLocationsRepository CompanyLocations { get; private set; }
        public ILanguageRepository Languages { get; private set; }
        public ISkillRepository Skills { get; private set; }
        public IMessagesRepository Messages { get; private set; }

        public UnitOfWork(IDataContext dataContext)
        {
            _context = dataContext;
            Pages = new PageRepository(_context);
            Users = new UserRepository(_context);
            Connections = new ConnectionsRepository(_context);
            Experiences = new ExperienceRepository(_context);
            Educations = new EducationRepository(_context);
            Institutions = new InstitutionRepository(_context);
            Licenses = new LicensesRepository(_context);
            CompanyLocations = new CompanyLocationsRepository(_context);
            Languages = new LanguageRepository(_context);
            Skills = new SkillRepository(_context);
            Messages = new MessagesRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
