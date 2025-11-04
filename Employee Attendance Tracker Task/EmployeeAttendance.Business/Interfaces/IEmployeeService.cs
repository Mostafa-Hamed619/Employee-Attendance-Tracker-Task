using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Data.Wrappers;

namespace EmployeeAttendance.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<PageResult<Employee>> GetPaginatedEmployee(int page, int pageSize);
        Task<Employee> GetEmployeeById(int id);

        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsEmailUniqueAsync(int id, string email);
        Task<EmployeeAttendanceSummary> GetAttedanceSummary(int employeeId);
    }
}
