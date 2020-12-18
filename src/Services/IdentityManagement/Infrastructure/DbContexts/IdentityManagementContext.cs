using IdentityManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Infrastructure.DbContexts
{
    public class IdentityManagementContext : DbContext
    {
        public IdentityManagementContext(DbContextOptions<IdentityManagementContext> options)
        : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}