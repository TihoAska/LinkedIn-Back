namespace LinkedIn.Models.ProfileDetails.Skills
{
    public class SkillForUserCreateRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SkillDomain { get; set; }
    }
}
