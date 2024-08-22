using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LinkedIn.Models.Users
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [MinLength(6, ErrorMessage = "Password needs to have a minimum of 6 characters")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Entered passwords don't match")]
        public string? ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "Invalid phone format")]
        public string? PhoneNumber { get; set; }
        public string? Job { get; set; }
        public string? Education { get; set; }

        [JsonIgnore]
        public string? ImageUrl { get; set; }
    }
}
