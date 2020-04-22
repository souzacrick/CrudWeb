using CrudPoc.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrudPoc.Repository
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions<CrudDbContext> options) : base(options)
        {

        }

        public DbSet<AnnouncementWebMotors> AnnouncementWebMotors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CrudConfiguration());
        }
    }
}