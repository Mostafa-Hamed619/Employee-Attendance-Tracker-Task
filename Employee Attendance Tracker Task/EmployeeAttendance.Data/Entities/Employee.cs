using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendance.Data.Entities;

public class Employee
{
    public int Id { get; set; }

    [Required]
    public int Code { get; set; } // will get it's value from services

    [Required, RegularExpression(@"^([A-Za-z]{2,}\s){3}[A-Za-z]{2,}$")]
    public string FullName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    [ValidateNever]
    public Department Department { get; set; }

    [ValidateNever]
    public virtual ICollection<Attendance> AttendanceRecords { get; set; }
}
