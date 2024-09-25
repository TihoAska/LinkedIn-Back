namespace LinkedIn.Models.ProfileDetails.Images
{
    public class BackgroundImageUpdateRequest
    {
        public int UserId { get; set; }
        public string ImageData { get; set; } = "../../../assets/images/profileMisc/timeline.png";
    }
}
