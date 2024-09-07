using LinkedIn.Data;
using LinkedIn.Models.ProfileDetails.Languages;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class LanguageRepository : GenericRepository<UserLanguages>, ILanguageRepository
    {
        public LanguageRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }
        public Task<UserLanguages> GetById(int id, CancellationToken cancellationToken)
        {
            return _query.Where(language => language.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
