using LinkedIn.ConnectionWebSocketHandler;
using LinkedIn.Models.Messages;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        IMessagesService _messagesService;
        WebSocketHandler _websocketHandler;

        public MessagesController(IMessagesService messagesService, WebSocketHandler webSocketHandler)
        {
            _messagesService = messagesService;
            _websocketHandler = webSocketHandler;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMessagesForUser(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _messagesService.GetAllMessagesForUser(userId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(MessageCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _messagesService.SendMessage(createRequest, cancellationToken);
                await _websocketHandler.NotifyUserOfNewEvent(createRequest.ReceiverId, result, "message");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
