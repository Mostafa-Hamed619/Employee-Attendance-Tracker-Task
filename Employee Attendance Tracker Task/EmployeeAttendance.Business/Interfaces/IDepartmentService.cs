using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Data.Wrappers;

namespace EmployeeAttendance.Business.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> CreateAsync(Department department);
        Task<Department> UpdateAsync(Department department);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Department>> GetAllDepartments();

        Task<PageResult<Department>> GetPaginatedDepartments(int page, int pageSize);
        Task<Department> GetDepartmentById(int id);
        Task<int> GetDepartEmployeeCount(int DepartmentId);

    }
}
