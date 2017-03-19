using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectMe.Api.Models
{
    public class Worker
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public string WorkerTypeId { get; set; }

        [ForeignKey("UserId")]
        protected virtual ApplicationUser Users { get; set; }

        [ForeignKey("WorkerTypeId")]
        protected virtual WorkerType WorkerType { get; set; }
    }
}
