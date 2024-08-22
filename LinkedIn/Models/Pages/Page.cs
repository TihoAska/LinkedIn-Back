using LinkedIn.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.Pages
{
    public class Page
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfFollowers { get; set; }
        public string? ImageUrl { get; set; }
        public List<User>? Followers { get; set; }
    }
}
