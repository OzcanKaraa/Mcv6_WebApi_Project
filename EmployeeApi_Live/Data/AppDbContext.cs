using Microsoft.EntityFrameworkCore;

namespace EmployeeApi_Live.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
