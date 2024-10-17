namespace LinkedIn.Models.Posts
{
    public class CommentReactionCreateRequest
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string ReactionType { get; set; }
    }
}
