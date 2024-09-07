using LinkedIn.Models.ProfileDetails.Languages;

namespace LinkedIn.Repository.IRepository
{
    public interface ILanguageRepository : IGenericRepository<UserLanguages>
    {
        Task<UserLanguages> GetById(int id, CancellationToken cancellationToken);
    }
}
