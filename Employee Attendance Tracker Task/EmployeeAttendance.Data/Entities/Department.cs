using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendance.Data.Entities;

public class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Department name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Department name must be between 3 and 50 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Department code is required.")]
    [RegularExpression(@"^[A-Z]{4}$", ErrorMessage = "Department code must be exactly 4 uppercase letters (e.g., HRMG, TECH).")]
    public string Code { get; set; }

    [Required(ErrorMessage = "Location is required.")]
    [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
    public string Location { get; set; }

    [ValidateNever]
    public ICollection<Employee> Employees { get; set; }
}
