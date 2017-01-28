using ConnectMe.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectMe.Api.Data
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public DatabaseContext() : base() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
