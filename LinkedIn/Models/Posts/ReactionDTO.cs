using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.Posts
{
    public class ReactionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public UserPost? UserPost { get; set; }
        public DateTime TimeReacted { get; set; }
        public ReactionTypeDTO? Type { get; set; }
    }
}
