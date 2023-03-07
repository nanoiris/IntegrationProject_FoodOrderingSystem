using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Models
{
    public partial class AppCtx : IdentityDbContext<AppUser>
    {
        public AppCtx() : base() { }
        public AppCtx(DbContextOptions<AppCtx> options) : base(options) { }

        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }
}
