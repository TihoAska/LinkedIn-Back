namespace LinkedIn.Models.ProfileDetails.LicensesAndCerfitications
{
    public class LicenseUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IssuingOrganization { get; set; }
        public DateTime IssueDate { get; set; }
        public string CredentialId { get; set; }
        public string CredentialUrl { get; set; }
    }
}
