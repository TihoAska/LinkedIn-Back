using LinkedIn.Models.ProfileDetails.Educations;

namespace LinkedIn.Repository.IRepository
{
    public interface IInstitutionRepository : IGenericRepository<Institution>
    {
        public Task<Institution> GetByName(string name, CancellationToken cancellationToken);
    }
}
