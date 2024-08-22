namespace LinkedIn.Models.ProfileDetails.Skills
{
    public class SkillsCreateRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyNameWhereSkillWasEarned { get; set; }
    }
}
