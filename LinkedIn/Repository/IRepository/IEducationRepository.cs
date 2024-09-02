using LinkedIn.Models.ProfileDetails.Educations;

namespace LinkedIn.Repository.IRepository
{
    public interface IEducationRepository : IGenericRepository<UserEducation>
    {
        Task<UserEducation> GetById(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<UserEducation>> GetAllByUserId(int userId, CancellationToken cancellationToken);
    }
}
