using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Data.Wrappers;

namespace EmployeeAttendance.Business.Interfaces
{
    namespace EmployeeAttendance.Business.Interfaces
    {
        public interface IAttendanceService
        {
            Task<IEnumerable<Attendance>> GetAllAsync();
            Task<PageResult<Attendance>> GetPaginatedAsync(int page, int pageSize, int? departmentId = null, int? employeeId = null, DateTime? from = null, DateTime? to = null);
            Task<Attendance?> GetByIdAsync(int id);
            Task<Attendance> MarkAttendanceAsync(int employeeId, DateTime date, AttendanceStatus status);
            Task<Attendance> UpdateAsync(Attendance attendance);
            Task<bool> DeleteAsync(int id);
            Task<Attendance?> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
            Task<bool> AttendanceExistsAsync(int employeeId, DateTime date);
        }
    }

}
