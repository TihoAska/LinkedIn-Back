

using LinkedIn.Models.ProfileDetails.Locations;

namespace LinkedIn.Models.ProfileDetails.Experiences
{
    public class ExperienceCreateRequest
    {
        public int UserId { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public string EmploymentType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public CompanyLocation Location { get; set; }
        public string LocationType { get; set; }
    }
}
