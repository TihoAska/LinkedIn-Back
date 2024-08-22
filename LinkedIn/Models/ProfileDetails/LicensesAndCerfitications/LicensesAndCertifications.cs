using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.ProfileDetails.LicensesAndCerfitications
{
    public class LicensesAndCertifications
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Name { get; set; }
        public string IssuingOrganization { get; set; }
        public DateTime IssueDate { get; set; }
        public string CredentialId { get; set; }
        public string OrganizationImageUrl { get; set; }
    }
}
