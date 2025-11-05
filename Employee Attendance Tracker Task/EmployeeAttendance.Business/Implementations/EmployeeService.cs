using EmployeeAttendance.Business.Interfaces;
using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Data.Wrappers;
using EmployeeAttendance.DataContext.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendance.Business.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _db;

        public EmployeeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            var checkNameCondition = await CheckNameConditions(employee);

            if (!checkNameCondition)
            {
                throw new Exception("The name must be 4 words within at least 2 letters");
            }

            employee.Code = await GetUniqueCode();
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return true;
        }



        public async Task<EmployeeAttendanceSummary> GetAttedanceSummary(int employeeId)
        {
            var now = DateTime.Now;
            var records = await _db.Attendances
                .Where(a => a.EmployeeId == employeeId && a.Date.Month == now.Month && a.Date.Year == now.Year)
                .ToListAsync();

            int present = records.Count(r => r.Status == AttendanceStatus.Present);
            int absent = records.Count(r => r.Status == AttendanceStatus.Absent);
            int total = present + absent;

            return new EmployeeAttendanceSummary
            {
                Presents = present,
                Absents = absent,
                AttendancePercentage = total == 0 ? 0 : Math.Round((double)present / total * 100, 2)
            };
        }



        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id) ?? null!;
        }


        public async Task<PageResult<Employee>> GetPaginatedEmployee(int page, int pageSize)
        {
            var query = _db.Employees.Include(e => e.Department).OrderBy(d => d.FullName).AsQueryable();

            return await query.ToPaginatedListAsync(page, pageSize);
        }


        public async Task<bool> IsEmailUniqueAsync(int id, string email)
        {
            return !await _db.Employees.AnyAsync(e => e.Id != id && e.Email == email);
        }



        public async Task<Employee> UpdateAsync(Employee employee)
        {


            var checkNameCondition = await CheckNameConditions(employee);

            if (!checkNameCondition)
            {
                throw new Exception("The name must be 4 words within at least 2 letters");
            }

            var existing = await _db.Employees.FindAsync(employee.Id);

            existing.FullName = employee.FullName;
            existing.Email = employee.Email;
            existing.DepartmentId = employee.DepartmentId;

            await _db.SaveChangesAsync();
            return existing;
        }



        #region Helping Method

        private async Task<int> GetUniqueCode()
        {
            int maxCode = 0;

            if (await _db.Employees.AnyAsync())
                maxCode = await _db.Employees.MaxAsync(e => e.Code);

            return maxCode + 1;

        }


        private async Task<bool> CheckNameConditions(Employee employee)
        {
            var nameArray = employee.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameArray.Length != 4 || nameArray.Any(n => n.Length < 2) || nameArray == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _db.Employees.ToListAsync();
        }
        #endregion
    }
}
