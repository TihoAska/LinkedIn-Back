namespace LinkedIn.Models.Posts
{
    public class PostReactionModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string ReactionType { get; set; }
    }
}
