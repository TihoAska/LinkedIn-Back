using LinkedIn.Models.ProfileDetails.Locations;

namespace LinkedIn.Repository.IRepository
{
    public interface ICompanyLocationsRepository : IGenericRepository<CompanyLocation>
    {
        Task<CompanyLocation> GetCompanyLocationByCityName(string cityName, CancellationToken cancellationToken);
        Task<CompanyLocation> GetCompanyLocationByLocationId(int locationId, CancellationToken cancellationToken);
    }
}
