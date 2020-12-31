using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PromocodesApp.Authentication;

namespace PromocodesApp.Models
{
    public class PromocodesAppContext : IdentityDbContext<ApplicationUser>
    {
        public PromocodesAppContext(DbContextOptions<PromocodesAppContext> options) : base(options) { }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<CodeServiceUser> CodesServicesUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeServiceUser>()
               .HasKey(x => new { x.CodeId, x.ServiceId, x.UserName });

            base.OnModelCreating(modelBuilder);
        }
    }
}
