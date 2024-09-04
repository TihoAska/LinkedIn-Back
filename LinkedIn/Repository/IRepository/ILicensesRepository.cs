using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;

namespace LinkedIn.Repository.IRepository
{
    public interface ILicensesRepository : IGenericRepository<LicensesAndCertifications>
    {
        Task<LicensesAndCertifications> GetById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<LicensesAndCertifications>> GetAllByUserId (int userId, CancellationToken cancellationToken);
    }
}
