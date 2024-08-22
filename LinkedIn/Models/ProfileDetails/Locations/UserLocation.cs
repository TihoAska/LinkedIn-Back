using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.ProfileDetails.Locations
{
    public class UserLocation
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
