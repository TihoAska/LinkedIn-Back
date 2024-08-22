﻿namespace LinkedIn.Models.ProfileDetails.LicensesAndCerfitications
{
    public class LicensesCreateRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string IssuingOrganization { get; set; }
        public DateTime IssueDate { get; set; }
        public string CredentialId { get; set; }
    }
}
