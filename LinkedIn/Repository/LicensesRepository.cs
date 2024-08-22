using LinkedIn.Data;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class LicensesRepository : GenericRepository<LicensesAndCertifications>, ILicensesRepository
    {
        public LicensesRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<IEnumerable<LicensesAndCertifications>> GetAllByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _query.Where(license => license.UserId == userId).ToListAsync(cancellationToken);
        }
    }
}
