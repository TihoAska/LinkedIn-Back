using AutoMapper;
using LinkedIn.Models.Pages;
using LinkedIn.Models.Users;
using LinkedIn.Repository.IRepository;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace LinkedIn.Services
{
    public class PageService : IPageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _autoMapper;

        public PageService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<Page>> GetAll(CancellationToken cancellationToken)
        {
            return await _unitOfWork.Pages.GetAll(cancellationToken);
        }

        public async Task<IEnumerable<Page>> GetAllPagesForUser(int userId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Pages.GetAllForUser(userId, cancellationToken);
        }

        public async Task<Page> GetById(int id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Pages.GetById(id, cancellationToken);
        }

        public async Task<Page> GetByName(string name, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Pages.GetByName(name, cancellationToken);
        }

        public async Task<IEnumerable<Page>> GetPagesYouMightLike(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetAllPages(id, cancellationToken) ?? throw new Exception("User with the given id was not found");
            var pagesFromDb = await _unitOfWork.Pages.GetAll(cancellationToken);

            var usersPages = userFromDb.PagesFollowing;

            if(usersPages == null)
            {
                return pagesFromDb;
            }

            return pagesFromDb.Where(page => !usersPages.Contains(page));
        }

        public async Task<IEnumerable<Page>> Get2PagesYouMightLike(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetAllPages(id, cancellationToken) ?? throw new Exception("User with the given id was not found");
            var pagesFromDb = await _unitOfWork.Pages.GetPagesYouMightLike(id, cancellationToken);

            return pagesFromDb;
        }

        public async Task<IdentityResult> Follow(FollowPageRequest followRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(followRequest.userId, cancellationToken) ?? throw new Exception("User not found!");
            var pageFromDb = await _unitOfWork.Pages.GetByName(followRequest.pageName, cancellationToken) ?? throw new Exception("Page was not found!");

            if (pageFromDb.Followers == null)
            {
                pageFromDb.Followers = new List<User>();
            }

            if (pageFromDb.Followers.Contains(userFromDb))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserAldreadyFollowsPage",
                    Description = "User already follows the page!",
                });
            }

            pageFromDb.Followers.Add(userFromDb);
            pageFromDb.NumberOfFollowers++;

            if(userFromDb.PagesFollowing == null)
            {
                userFromDb.PagesFollowing = new List<Page>();
            }

            userFromDb.PagesFollowing.Add(pageFromDb);

            await _unitOfWork.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> Unfollow(int pageId, int userId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(userId, cancellationToken) ?? throw new Exception("User not found!");
            var pageFromDb = await _unitOfWork.Pages.GetById(pageId, cancellationToken) ?? throw new Exception("Page was not found!");

            if (pageFromDb.Followers.Contains(userFromDb))
            {
                pageFromDb.Followers.Remove(userFromDb);
                pageFromDb.NumberOfFollowers--;

                userFromDb.PagesFollowing.Remove(pageFromDb);

                await _unitOfWork.SaveChangesAsync();

                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserDoesntFollowThePage",
                Description = "User doesn't follow the page!",
            });
        }

        public async Task<Page> Create(PageCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var newPage = _autoMapper.Map<Page>(createRequest);

            _unitOfWork.Pages.Add(newPage);
            await _unitOfWork.SaveChangesAsync();

            return newPage;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var pageFromDb = await _unitOfWork.Pages.GetById(id, cancellationToken) ?? throw new Exception("Page was not found!");

            _unitOfWork.Pages.Remove(pageFromDb);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
