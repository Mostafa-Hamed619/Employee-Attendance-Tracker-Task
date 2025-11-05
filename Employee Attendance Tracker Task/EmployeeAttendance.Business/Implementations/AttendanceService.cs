using EmployeeAttendance.Business.Interfaces.EmployeeAttendance.Business.Interfaces;
using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Data.Wrappers;
using EmployeeAttendance.DataContext.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendance.Business.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AppDbContext _db;

        public AttendanceService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _db.Attendances
                .Include(a => a.Employee).ThenInclude(e => e.Department)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<PageResult<Attendance>> GetPagedAttendances(int page, int pageSize)
        {
            var query = _db.Attendances.Include(a => a.Employee).ThenInclude(a => a.Department).AsQueryable();

            return await query.ToPaginatedListAsync(page, pageSize);
        }


        public async Task<Attendance> GetByIdAsync(int id)
        {
            return await _db.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Attendance> CreateOrUpdateAsync(int employeeId, DateTime date, AttendanceStatus status)
        {
            if (date.Date > DateTime.Today)
                throw new Exception("Cannot mark attendance for future dates.");

            var existing = await _db.Attendances
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date.Date == date.Date);

            if (existing != null)
            {
                existing.Status = status;
            }
            else
            {
                existing = new Attendance
                {
                    EmployeeId = employeeId,
                    Date = date.Date,
                    Status = status
                };
                _db.Attendances.Add(existing);
            }

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<Attendance> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _db.Attendances
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date.Date == date.Date);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _db.Attendances.FindAsync(id);
            if (record == null) return false;

            _db.Attendances.Remove(record);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Attendance>> FilterAsync(int? departmentId, int? employeeId, DateTime? from, DateTime? to)
        {
            var query = _db.Attendances
                .Include(a => a.Employee)
                .ThenInclude(e => e.Department)
                .AsQueryable();

            if (departmentId.HasValue)
                query = query.Where(a => a.Employee.DepartmentId == departmentId);

            if (employeeId.HasValue)
                query = query.Where(a => a.EmployeeId == employeeId);

            if (from.HasValue)
                query = query.Where(a => a.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(a => a.Date <= to.Value);

            return await query.OrderByDescending(a => a.Date).ToListAsync();
        }
    }
}
