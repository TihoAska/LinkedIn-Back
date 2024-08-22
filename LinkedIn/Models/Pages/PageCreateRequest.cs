using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LinkedIn.Models.Pages
{
    public class PageCreateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        public string ImageUrl { get; set; }

        [JsonIgnore]
        public int NumberOfFollowers { get; set; } = 0;
    }
}
