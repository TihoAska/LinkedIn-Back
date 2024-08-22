using Microsoft.AspNetCore.Identity;
using LinkedIn.Models.Pages;
using System.ComponentModel.DataAnnotations.Schema;
using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Skills;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.Languages;

namespace LinkedIn.Models.Users
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ImageUrl { get; set; }
        public string? Job { get; set; }
        public List<UserEducation>? Education { get; set; }
        public List<LicensesAndCertifications>? LicensesAndCertifications { get; set; }
        public List<UserSkills>? Skills { get; set; }
        public List<Experience>? Experience { get; set; }
        public List<UserLanguages>? Languages { get; set; }
        public int ProfileDetailsId { get; set; }

        [ForeignKey("ProfileDetailsId")]
        public ProfileDetails ProfileDetails { get; set; }
        public List<User>? Connections { get; set; }
        public List<User>? Followers { get; set; }
        public List<User>? Following {  get; set; }
        public List<Page>? PagesFollowing { get; set; }
        public List<PendingConnections>? SentConnections { get; set; }
        public List<PendingConnections>? ReceivedConnections { get; set; }
    }
}
