using EmployeeAttendance.Business.Interfaces;
using EmployeeAttendance.Data.Data;
using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Data.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendance.Business.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _db;

        public DepartmentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Department> CreateAsync(Department department)
        {
            bool isUnique = await IsDepartmentUniqueAsync(department.Name, department.Code);

            if (!isUnique)
                throw new InvalidOperationException("Department name or code already exists.");

            await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();
            return department;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _db.Departments.FindAsync(id);
            if (department == null)
                return false;

            _db.Departments.Remove(department);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _db.Departments.Include(d => d.Employees).ToListAsync();
        }

        public async Task<int> GetDepartEmployeeCount(int DepartmentId)
        {
            return await _db.Employees.CountAsync(e => e.DepartmentId == DepartmentId);
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var Dept = await _db.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == id);
            if (Dept == null)
                return null;
            return Dept;
        }

        public async Task<PageResult<Department>> GetPaginatedDepartments(int page, int pageSize)
        {
            var query = _db.Departments.AsQueryable();

            return await query.ToPaginatedListAsync(page, pageSize);
        }

        public async Task<Department> UpdateAsync(Department department)
        {
            var existing = await _db.Departments.FindAsync(department.Id);
            if (existing == null) return null!;



            bool isUnique = !await _db.Departments.AnyAsync(d => (d.Code == department.Code || d.Name == department.Name) && d.Id != department.Id);

            if (!isUnique)
                throw new InvalidOperationException("Department name or code already exists.");

            existing.Name = department.Name;
            existing.Code = department.Code;
            existing.Location = department.Location;

            await _db.SaveChangesAsync();
            return existing;
        }



        #region Helping Mehtods

        private async Task<bool> IsDepartmentUniqueAsync(string departmentName, string code)
        {
            return !await _db.Departments.AnyAsync(d => d.Code == code || d.Name == departmentName);
        }


        #endregion
    }
}
