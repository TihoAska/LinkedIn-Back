using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.ProfileDetails.Skills
{
    public class UserSkills
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SkillsImageUrl { get; set; }
    }
}
