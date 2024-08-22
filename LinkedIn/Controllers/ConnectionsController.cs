using LinkedIn.Services;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace LinkedIn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private IUserService _userService;

        public ConnectionsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingConnections(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAllPendingConnections(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUserConnections(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAllUserConnections(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendConnection(int senderId, int receiverId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.SendConnection(senderId, receiverId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> AcceptConnection(int senderId, int receiverId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.AcceptConnection(senderId, receiverId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> RejectConnection(int senderId, int receiverId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.RejectConnection(senderId, receiverId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
