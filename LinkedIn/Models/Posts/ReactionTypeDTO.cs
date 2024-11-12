namespace LinkedIn.Models.Posts
{
    public class ReactionTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IconUrl { get; set; } = "../../../assets/images/icons/like.png";
    }
}
