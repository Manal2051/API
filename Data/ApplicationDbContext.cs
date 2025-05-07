using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TestToken.Models;

namespace TestToken.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Property(p => p.FirstName).IsRequired();
            builder.Entity<ApplicationUser>().Property(p => p.LastName).IsRequired();
            builder.Entity<ApplicationUser>().Property(p => p.Address).HasMaxLength(200);



        }
    }
}
