using LinkedIn.Models.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.ProfileDetails.Languages
{
    public class UserLanguages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Proficiency { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
