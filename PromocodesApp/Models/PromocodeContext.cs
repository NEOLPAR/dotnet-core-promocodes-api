using Microsoft.EntityFrameworkCore;

namespace PromocodesApp.Models
{
    public class PromocodesAppContext : DbContext
    {
        public PromocodesAppContext(DbContextOptions<PromocodesAppContext> options) : base(options) { }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
