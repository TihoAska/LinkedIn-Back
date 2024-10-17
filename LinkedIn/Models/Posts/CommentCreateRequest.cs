namespace LinkedIn.Models.Posts
{
    public class CommentCreateRequest
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
    }
}
