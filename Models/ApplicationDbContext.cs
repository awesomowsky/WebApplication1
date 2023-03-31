using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUsers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Items> Items { get; set; }
        public DbSet<Commnents> Commnents { get; set; }

    }
}
