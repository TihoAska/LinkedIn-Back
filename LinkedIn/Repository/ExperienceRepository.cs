using LinkedIn.Data;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Repository.IRepository;

namespace LinkedIn.Repository
{
    public class ExperienceRepository : GenericRepository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }
    }
}
