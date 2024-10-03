namespace LinkedIn.Models.Messages
{
    public class MessageCreateRequest
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
