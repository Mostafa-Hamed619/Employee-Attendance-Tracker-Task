using EmployeeAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendance.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Code)
                .IsUnique();


            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Code)
                .IsUnique();

            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.EmployeeId, a.Date })
                .IsUnique();

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // --- Departments ---
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Human Resources", Code = "HRMG", Location = "Cairo" },
                new Department { Id = 2, Name = "Technology", Code = "TECH", Location = "Alexandria" },
                new Department { Id = 3, Name = "Finance", Code = "FINA", Location = "Giza" }
            );

            // --- Employees ---
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Code = 1001, FullName = "Mostafa Elsayed Mostafa Hamed", Email = "mostafa.hamed@example.com", DepartmentId = 2 },
                new Employee { Id = 2, Code = 1002, FullName = "Omar Hassan Mohamed Ali", Email = "omar.hassan@example.com", DepartmentId = 1 },
                new Employee { Id = 3, Code = 1003, FullName = "Sara Mahmoud Adel Nabil", Email = "sara.mahmoud@example.com", DepartmentId = 3 }
            );

            // --- Attendance ---
            modelBuilder.Entity<Attendance>().HasData(
                new Attendance { Id = 1, EmployeeId = 1, Date = new DateTime(2025, 11, 1), Status = AttendanceStatus.Present },
                new Attendance { Id = 2, EmployeeId = 1, Date = new DateTime(2025, 11, 2), Status = AttendanceStatus.Absent },
                new Attendance { Id = 3, EmployeeId = 2, Date = new DateTime(2025, 11, 1), Status = AttendanceStatus.Present },
                new Attendance { Id = 4, EmployeeId = 3, Date = new DateTime(2025, 11, 1), Status = AttendanceStatus.Present }
            );
        }


    }
}
