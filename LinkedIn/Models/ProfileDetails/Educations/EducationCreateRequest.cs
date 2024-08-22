using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LinkedIn.Models.ProfileDetails.Educations
{
    public class EducationCreateRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
