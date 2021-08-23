using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public ApplicationContext() { }

        public DbSet<File> Files { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}
