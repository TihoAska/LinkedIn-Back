using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn.Models.Users
{
    public class PendingConnections
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = "Pending";

        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }
    }
}
