using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.Posts
{
    public class UserPost
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string PostImage { get; set; }
        public int PosterId { get; set; }

        [ForeignKey("PosterId")]
        public User User{ get; set; }
        public List<Reaction>? Reactions { get; set; }
        public int NumberOfReactions { get; set; } = 0;
        public List<Comment>? Comments { get; set; }
        public int NumberOfComments { get; set; } = 0;
        public DateTime TimePosted { get; set; }
        public bool IsEdited { get; set; } = false;
    }
}
