using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Faculty_Portal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Faculty_Portal.Models.Dean>? Deans { get; set; }
        public DbSet<Faculty_Portal.Models.HOD>? Hods { get; set; }
        public DbSet<Faculty_Portal.Models.Department>? Departments { get; set; }
        public DbSet<Faculty_Portal.Models.FacultyStaff>? FacultyStaffs { get; set; }
        public DbSet<Faculty_Portal.Models.DepartmentStaff>? DepartmentStaff { get; set; }
        public DbSet<Faculty_Portal.Models.Newsletter>? Newsletters { get; set; }
        public DbSet<Faculty_Portal.Models.Subscription>? Subscriptions { get; set; }
    }
}