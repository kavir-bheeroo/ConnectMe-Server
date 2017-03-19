using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ConnectMe.Api.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsWorker { get; set; }
    }
}
