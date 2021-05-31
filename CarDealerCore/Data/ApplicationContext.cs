using Microsoft.EntityFrameworkCore;
using CarDealerCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarDealerCore.Data
{
    public sealed class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Sale> Sales{ get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            
        }

    }
}
