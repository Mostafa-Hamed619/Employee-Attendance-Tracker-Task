using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendance.Data.Entities;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Employee code is required.")]
    public int Code { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    [RegularExpression(@"^([A-Za-z]{2,}\s){3}[A-Za-z]{2,}$", ErrorMessage = "Full name must contain exactly four words, each at least two letters long.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Department is required.")]
    public int DepartmentId { get; set; }

    [ValidateNever]
    public Department Department { get; set; }

    [ValidateNever]
    public virtual ICollection<Attendance> AttendanceRecords { get; set; }
}
