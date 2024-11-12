using LinkedIn.Models.Pages;
using LinkedIn.Models.ProfileDetails.Locations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Services.IServices
{
    public interface IPageService
    {
        Task<IEnumerable<Page>> GetAll(CancellationToken cancellationToken);
        Task<Page> GetById(int id, CancellationToken cancellationToken);
        Task<Page> GetByName(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Page>> GetAllPagesForUser(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<Page>> GetPagesYouMightLike(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Page>> Get2PagesYouMightLike(int id, CancellationToken cancellationToken);
        Task<IdentityResult> Follow(FollowPageRequest followRequest, CancellationToken cancellationToken);
        Task<IdentityResult> Unfollow([FromQuery] int userId, [FromQuery] string pageName, CancellationToken cancellationToken);
        Task<Page> Create(PageCreateRequest createRequest, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
        Task<CompanyLocation> GetCompanyLocationByCityName(string cityName, CancellationToken cancellationToken);
        Task<IEnumerable<CompanyLocation>> GetAllCompanyLocations(CancellationToken cancellationToken);
        Task<CompanyLocation> GetCompanyLocationByLocationId(int locationId, CancellationToken cancellationToken);
    }
}
