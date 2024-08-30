using LinkedIn.Models.ProfileDetails.Locations;

namespace LinkedIn.Models.ProfileDetails.Experiences
{
    public class ExperienceUpdateRequest
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string EmploymentType { get; set; }
        public string CompanyName { get; set; }
        public CompanyLocation Location { get; set; }
        public string LocationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
