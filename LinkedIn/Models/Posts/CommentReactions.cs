using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.Posts
{
    public class CommentReactions
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int CommentId { get; set; }

        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }
        public int ReactionTypeId { get; set; }

        [ForeignKey("ReactionTypeId")]
        public ReactionType ReactionType { get; set; }
    }
}
