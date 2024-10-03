using LinkedIn.JWTFeatures;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace LinkedIn.ConnectionWebSocketHandler
{
    public class WebSocketHandler
    {
        private static readonly Dictionary<int, WebSocket> UserWebSockets = new Dictionary<int, WebSocket>();
        private JWTHandler _jwtHandler;

        public WebSocketHandler(JWTHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        public async Task HandleAsync(HttpContext context)
        {
            var token = context.Request.Query["accessToken"].ToString();
            var claimsPrincipal = _jwtHandler.DecodeJwtToken(token);
            

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            int userId = int.Parse(claimsPrincipal.FindFirst("Id")?.Value);

            UserWebSockets[userId] = socket;

            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var buffer = new byte[1024 * 4];
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        UserWebSockets.Remove(userId);
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket error: {ex.Message}");
                UserWebSockets.Remove(userId);
            }
        }

        public async Task NotifyUserOfNewEvent(int userId, object newConnectionData, string eventType)
        {
            if (UserWebSockets.TryGetValue(userId, out var socket))
            {
                if (socket != null && socket.State == WebSocketState.Open)
                {
                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                        WriteIndented = true,
                    };

                    var eventData = new
                    {
                        EventType = eventType,
                        Data = newConnectionData
                    };

                    var message = JsonSerializer.Serialize(eventData, options);
                    var bytes = Encoding.UTF8.GetBytes(message);
                    await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    Console.WriteLine($"WebSocket for user {userId} is either closed or null.");
                }
            }
            else
            {
                Console.WriteLine($"No WebSocket found for user {userId}.");
            }
        }
    }
}
