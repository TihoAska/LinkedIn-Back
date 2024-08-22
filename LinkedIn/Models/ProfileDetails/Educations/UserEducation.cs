using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.ProfileDetails.Educations
{
    public class UserEducation
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SchoolImageUrl { get; set; } = "../../../assets/images/pageLogos/education-default.png";
        public int InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public Institution Institution { get; set; }
    }
}
