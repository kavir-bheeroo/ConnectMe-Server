using ConnectMe.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectMe.Api.Data
{
    public interface IDatabaseContext
    {
        DbSet<UserInfo> UserInfo { get; set; }

        int SaveChanges();
    }
}
