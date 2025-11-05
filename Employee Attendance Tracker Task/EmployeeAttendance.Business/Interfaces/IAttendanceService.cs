using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Data.Wrappers;

namespace EmployeeAttendance.Business.Interfaces
{
    namespace EmployeeAttendance.Business.Interfaces
    {
        public interface IAttendanceService
        {
            Task<IEnumerable<Attendance>> GetAllAsync();
            Task<PageResult<Attendance>> GetPagedAttendances(int page, int pageSize);

            Task<Attendance> GetByIdAsync(int id);
            Task<Attendance> CreateOrUpdateAsync(int employeeId, DateTime date, AttendanceStatus status);
            Task<bool> DeleteAsync(int id);
            Task<Attendance> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
            Task<List<Attendance>> FilterAsync(int? departmentId, int? employeeId, DateTime? from, DateTime? to);

        }
    }

}
