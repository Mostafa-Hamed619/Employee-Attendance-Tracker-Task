using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendance.Data.Entities;

public enum AttendanceStatus
{
    Present,
    Absent
}

public class Attendance
{
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public AttendanceStatus Status { get; set; }
}
