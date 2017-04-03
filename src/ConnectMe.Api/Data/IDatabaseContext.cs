using ConnectMe.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectMe.Api.Data
{
    public interface IDatabaseContext
    {
        DbSet<UserInfo> UserInfo { get; set; }
        DbSet<Worker> Worker { get; set; }
        DbSet<WorkerType> WorkerType { get; set; }
        DbSet<Message> Message { get; set; }

        int SaveChanges();
    }
}
