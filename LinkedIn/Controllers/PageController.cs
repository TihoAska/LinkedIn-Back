using LinkedIn.Models.Pages;
using LinkedIn.Services;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.GetById(id, cancellationToken);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.GetAll(cancellationToken);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetByName(string name, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.GetByName(name, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPagesForUser(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.GetAllPagesForUser(userId, cancellationToken);
                return Ok(result); 
            } 
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPagesYouMightLike(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.GetPagesYouMightLike(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get2PagesYouMightLike(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.Get2PagesYouMightLike(userId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(PageCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.Create(createRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Follow([FromBody] FollowPageRequest followRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.Follow(followRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Unfollow(int pageId, int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.Unfollow(pageId, userId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.Delete(id, cancellationToken);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetCompanyLocationByCityName(string cityName, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.GetCompanyLocationByCityName(cityName, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCompanyLocations(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.GetAllCompanyLocations(cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetCompanyLocationByLocationId(int locationId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _pageService.GetCompanyLocationByLocationId(locationId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
