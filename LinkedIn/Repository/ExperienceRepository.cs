using LinkedIn.Data;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class ExperienceRepository : GenericRepository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public Task<Experience> GetExperienceById(int id, CancellationToken cancellationToken)
        {
            return _query.Where(experience => experience.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
