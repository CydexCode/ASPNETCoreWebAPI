using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPI.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options): base(options)
        {

        }
        public DbSet<Student> Students { get; set; } = null!;
    }
}
