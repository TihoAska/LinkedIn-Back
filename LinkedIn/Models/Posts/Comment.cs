using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.Posts
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public UserPost UserPost { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime TimeCommented { get; set; }
    }
}
