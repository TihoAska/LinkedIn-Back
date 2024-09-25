using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.Languages;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Skills;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.Users
{
    public class ProfileDetails
    {
        public int Id { get; set; }
        public int ProfileViews { get; set; }
        public int PostImpressions { get; set; }
        public int SearchAppearances { get; set; }
        public string BannerImage { get; set; } = "/assets/profileMisc/timeline.png";
    }
}
