using LinkedIn.Data;
using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class EducationRepository : GenericRepository<UserEducation>, IEducationRepository
    {
        public EducationRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<IEnumerable<UserEducation>> GetAllByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _query.Where(userEducation => userEducation.UserId == userId).ToListAsync(cancellationToken);
        }
    }
}
