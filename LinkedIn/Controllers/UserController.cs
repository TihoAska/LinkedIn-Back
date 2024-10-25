using LinkedIn.Models.Account;
using LinkedIn.Models.Users;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAll(cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllWithEducations(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAllWithEducations(cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetById(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetByUserName(string userName, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetByUserName(userName, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetByEmail(email, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetByPhoneNumber(phoneNumber, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUserFollowers(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAllUserFollowers(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUserFollowing(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAllUserFollowing(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUserFollowingPages(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAllUserFollowingPages(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetFiveUsers(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetFiveUsers(cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPeopleYouMayKnow(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetPeopleYouMayKnow(id, cancellationToken);
                return Ok(result);
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOtherSimilarProfiles(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetOtherSimilarProfiles(id, cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFiveOtherSimilarProfiles(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetFiveOtherSimilarProfiles(id, cancellationToken);
                return Ok(result);
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFivePeopleYouMayKnow(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetFivePeopleYouMayKnow(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdWithUserDetails(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetByIdWithUserDetails(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.Create(createRequest, cancellationToken);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest loginRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.Login(loginRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Refresh(RefreshTokenRequest refreshToken, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.Refresh(refreshToken, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> RequestPasswordResetToken(int userId)
        {
            try
            {
                var result = await _userService.RequestPasswordResetToken(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var result = await _userService.ResetPassword(resetPasswordRequest);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(UserUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.Update(updateRequest, cancellationToken);
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
                var result = await _userService.Delete(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
