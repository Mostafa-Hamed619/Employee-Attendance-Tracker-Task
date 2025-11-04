using EmployeeAttendance.Business.Interfaces;
using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeAttendance.Presentation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _empService;
        private readonly IDepartmentService _deptService;

        public EmployeeController(IEmployeeService empService, IDepartmentService deptService)
        {
            _empService = empService;
            _deptService = deptService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 7)
        {
            var pagedEmployees = await _empService.GetPaginatedEmployee(page, pageSize);

            var employeeVMs = new List<EmployeesTableViewModel>();

            foreach (var emp in pagedEmployees.items)
            {
                var summary = await _empService.GetAttedanceSummary(emp.Id);

                employeeVMs.Add(new EmployeesTableViewModel
                {
                    Id = emp.Id,
                    Code = emp.Code,
                    FullName = emp.FullName,
                    Email = emp.Email,
                    DepartmentName = emp.Department?.Name ?? "N/A",
                    Presents = summary.Presents,
                    Absents = summary.Absents,
                    AttendancePercentage = summary.AttendancePercentage
                });
            }

            ViewBag.PageNumber = pagedEmployees.Page;
            ViewBag.TotalPages = pagedEmployees.TotalPage;

            return View(employeeVMs);

        }


        public async Task<IActionResult> Create()
        {
            await ShareDepartments();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var isUnique = await _empService.IsEmailUniqueAsync(employee.Id, employee.Email);
            if (!isUnique)
            {
                TempData["Error"] = "This email already exists.";
                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _empService.CreateAsync(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            await ShareDepartments();
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _empService.GetEmployeeById(id);
            if (employee == null) return NotFound();

            await ShareDepartments();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            var isunique = await _empService.IsEmailUniqueAsync(id, employee.Email);
            if (!isunique)
            {
                TempData["Error"] = "This email already exists.";
                return RedirectToAction("Edit", new { Id = id });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _empService.UpdateAsync(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            await ShareDepartments();
            return View(employee);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _empService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _empService.DeleteAsync(id);

            if (!deleted)
            {
                TempData["Error"] = "Employee could not be deleted. It may not exist.";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Employee deleted successfully.";
            return RedirectToAction("Index");
        }


        private async Task ShareDepartments()
        {
            var departments = await _deptService.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
        }
    }
}
