namespace LinkedIn.Models.Posts
{
    public class PostUpdateRequest
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
