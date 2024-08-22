using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;

namespace LinkedIn.Repository.IRepository
{
    public interface ILicensesRepository : IGenericRepository<LicensesAndCertifications>
    {
        public Task<IEnumerable<LicensesAndCertifications>> GetAllByUserId (int userId, CancellationToken cancellationToken);
    }
}
