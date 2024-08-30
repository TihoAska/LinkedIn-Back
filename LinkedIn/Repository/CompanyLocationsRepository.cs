using LinkedIn.Data;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class CompanyLocationsRepository : GenericRepository<CompanyLocation>, ICompanyLocationsRepository
    {
        public CompanyLocationsRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public Task<CompanyLocation> GetCompanyLocationByCityName(string cityName, CancellationToken cancellationToken)
        {
            return _query.Where(companyLocation => companyLocation.City == cityName).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<CompanyLocation> GetCompanyLocationByLocationId(int locationId, CancellationToken cancellationToken)
        {
            return _query.Where(companyLocation => companyLocation.Id == locationId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
