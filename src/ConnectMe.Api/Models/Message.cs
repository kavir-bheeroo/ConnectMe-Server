using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectMe.Api.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }
        public string FromUserId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public string Body { get; set; }

        [ForeignKey("UserId")]
        protected virtual ApplicationUser Users { get; set; }

        [ForeignKey("FromUserId")]
        protected virtual ApplicationUser FromUsers { get; set; }
    }
}
