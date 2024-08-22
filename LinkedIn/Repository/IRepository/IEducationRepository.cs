using LinkedIn.Models.ProfileDetails.Educations;

namespace LinkedIn.Repository.IRepository
{
    public interface IEducationRepository : IGenericRepository<UserEducation>
    {
        public Task<IEnumerable<UserEducation>> GetAllByUserId(int userId, CancellationToken cancellationToken);
    }
}
