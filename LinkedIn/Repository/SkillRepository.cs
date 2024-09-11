using LinkedIn.Data;
using LinkedIn.Models.ProfileDetails.Skills;
using LinkedIn.Repository.IRepository;

namespace LinkedIn.Repository
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        public SkillRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }
    }
}
