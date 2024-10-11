namespace LinkedIn.Models.Posts
{
    public class PostCreateRequest
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
