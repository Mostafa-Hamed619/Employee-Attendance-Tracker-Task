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

        public AttendanceService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AttendanceExistsAsync(int employeeId, DateTime date)
        {
            return await _db.Attendances.AnyAsync(a => a.EmployeeId == employeeId && a.Date.Date == date.Date);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendance = await _db.Attendances.FindAsync(id);
            if (attendance == null)
                return false;

            _db.Attendances.Remove(attendance);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _db.Attendances.Include(a => a.Employee).ThenInclude(a => a.Department).OrderByDescending(a => a.Date).ToListAsync();
        }



        public async Task<Attendance?> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _db.Attendances.FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date.Date == date.Date);
        }


        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _db.Attendances.Include(a => a.Employee).ThenInclude(a => a.Department).FirstOrDefaultAsync(a => a.Id == id);
        }



        public async Task<PageResult<Attendance>> GetPaginatedAsync(int page, int pageSize, int? departmentId = null, int? employeeId = null, DateTime? from = null, DateTime? to = null)
        {
            var query = _db.Attendances
               .Include(a => a.Employee)
               .ThenInclude(e => e.Department)
               .AsQueryable();

            if (departmentId.HasValue)
                query = query.Where(a => a.Employee.DepartmentId == departmentId.Value);

            if (employeeId.HasValue)
                query = query.Where(a => a.EmployeeId == employeeId.Value);

            if (from.HasValue)
                query = query.Where(a => a.Date >= from.Value.Date);

            if (to.HasValue)
                query = query.Where(a => a.Date <= to.Value.Date);

            var totalCount = await query.CountAsync();

            query = query.OrderByDescending(a => a.Date);

            return await query.ToPaginatedListAsync(page, pageSize);

        }

        public async Task<Attendance> MarkAttendanceAsync(int employeeId, DateTime date, AttendanceStatus status)
        {
            if (date.Date > DateTime.Today)
                throw new Exception("Cannot mark attendance for future dates.");

            var existing = await GetByEmployeeAndDateAsync(employeeId, date);

            if (existing != null)
            {
                existing.Status = status;
                _db.Attendances.Update(existing);
                await _db.SaveChangesAsync();
                return existing;
            }

            var attendance = new Attendance
            {
                EmployeeId = employeeId,
                Date = date.Date,
                Status = status
            };

            _db.Attendances.Add(attendance);
            await _db.SaveChangesAsync();
            return attendance;

        }

        public async Task<Attendance> UpdateAsync(Attendance attendance)
        {
            var existing = await _db.Attendances.FindAsync(attendance.Id);
            if (existing == null)
                throw new Exception("Attendance record not found.");

            if (attendance.Date.Date > DateTime.Today)
                throw new Exception("Cannot update attendance for future dates.");

            existing.Status = attendance.Status;
            existing.Date = attendance.Date;
            existing.EmployeeId = attendance.EmployeeId;

            _db.Attendances.Update(existing);
            await _db.SaveChangesAsync();
            return existing;
        }
    }
}
