using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectMe.Api.Models
{
    public class UserInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string UserId { get; set; }
    }
}
