using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LinkedIn.Models.ProfileDetails.Locations
{
    public class CompanyLocation
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
    }
}
