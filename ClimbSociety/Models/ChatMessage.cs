using System.ComponentModel.DataAnnotations;

namespace ClimbSociety.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id{ get; set; }/* = Guid.NewGuid().ToString();*/
        public int User { get; set; }

        public int ConnectionId { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}
