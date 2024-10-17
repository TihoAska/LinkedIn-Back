using LinkedIn.ConnectionWebSocketHandler;
using LinkedIn.Models.Posts;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkedIn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly WebSocketHandler _websocketHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostsController(IPostsService postsService, WebSocketHandler websocketHandler, IHttpContextAccessor httpContextAccessor)
        {
            _postsService = postsService;
            _websocketHandler = websocketHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int postId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postsService.GetById(postId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllConnectionsPosts(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postsService.GetAllConnectionsPosts(userId, cancellationToken);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(PostCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postsService.Create(createRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(PostUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postsService.Update(updateRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] 
        public async Task<ActionResult> ReactOnPost(PostReactionModel reactionModel, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postsService.ReactOnPost(reactionModel, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CommentOnPost(CommentCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postsService.CommentOnPost(createRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ReactOnComment(CommentReactionCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postsService.ReactOnComment(createRequest, cancellationToken);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int postId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _postsService.Delete(postId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
