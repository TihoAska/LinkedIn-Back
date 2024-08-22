using LinkedIn.Models.Pages;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.ProfileDetails.Experiences
{
    public class Experience
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Position { get; set; }
        public Page Company { get; set; }
        public string CompanyImageUrl { get; set; } = "../../../assets/images/pageLogos/default-experience.png";
        public string EmploymentType { get; set; } = "Full-time";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CompanyLocationId { get; set; }

        [ForeignKey("CompanyLocationId")]
        public CompanyLocation Location { get; set; }
        public bool IsRemote { get; set; }
    }
}
