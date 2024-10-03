namespace LinkedIn.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPageRepository Pages { get; }
        IUserRepository Users { get; }
        IConnectionsRepository Connections { get; }
        IExperienceRepository Experiences { get; }
        IEducationRepository Educations { get; }
        IInstitutionRepository Institutions { get; }
        ILicensesRepository Licenses { get; }
        ICompanyLocationsRepository CompanyLocations { get; }
        ILanguageRepository Languages { get; }
        ISkillRepository Skills { get; }
        IMessagesRepository Messages { get; }
        Task<int> SaveChangesAsync();
    }
}
