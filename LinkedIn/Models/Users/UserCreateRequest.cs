using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LinkedIn.Models.Users
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password needs to have a minimum of 6 characters")]
        public string Password { get; set; }

        [Phone(ErrorMessage = "Invalid phone format")]
        public string? PhoneNumber { get; set; }

        public string? Job { get; set; }
        public string? Education { get; set; }

        [JsonIgnore]
        public string? ImageUrl { get; set; }
    }
}
