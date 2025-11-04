using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendance.Presentation.Models
{
    public class EmployeesTableViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Employee Code")]
        public int Code { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        [Display(Name = "Presents (This Month)")]
        public int Presents { get; set; }

        [Display(Name = "Absents (This Month)")]
        public int Absents { get; set; }

        [Display(Name = "Attendance %")]
        public double AttendancePercentage { get; set; }

    }
}
