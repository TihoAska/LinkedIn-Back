namespace LinkedIn.Models.ProfileDetails.Languages
{
    public class LanguagesCreateRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Proficiency { get; set; }

    }
}
