using Microsoft.EntityFrameworkCore;

namespace CsharpSampleWeb.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts)
        {
            
        }

        public DbSet<Person> Person { get; set; }
    }
}
