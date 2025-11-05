using EmployeeAttendance.Business.Interfaces;
using EmployeeAttendance.Business.Interfaces.EmployeeAttendance.Business.Interfaces;
using EmployeeAttendance.Data.Entities;
using EmployeeAttendance.Data.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_Attendance_Tracker.Controllers;

public class AttendanceController : Controller
{
    private readonly IAttendanceService _attendanceService;
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;

    public AttendanceController(
        IAttendanceService attendanceService,
        IEmployeeService employeeService,
        IDepartmentService departmentService)
    {
        _attendanceService = attendanceService;
        _employeeService = employeeService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index(int page = 1, int? departmentId = null, int? employeeId = null, DateTime? from = null, DateTime? to = null)
    {
        await PopulateFilters();

        const int pageSize = 3;

        PageResult<Attendance> pagedRecords;

        if (departmentId.HasValue || employeeId.HasValue || from.HasValue || to.HasValue)
        {
            var allFiltered = await _attendanceService.FilterAsync(departmentId, employeeId, from, to);
            var totalCount = allFiltered.Count();
            var items = allFiltered.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            pagedRecords = new PageResult<Attendance>
            {
                items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
        else
        {
            pagedRecords = await _attendanceService.GetPagedAttendances(page, pageSize);
        }

        ViewBag.CurrentPage = page;
        ViewBag.DepartmentId = departmentId;
        ViewBag.EmployeeId = employeeId;
        ViewBag.From = from?.ToString("yyyy-MM-dd");
        ViewBag.To = to?.ToString("yyyy-MM-dd");

        return View(pagedRecords);
    }

    [HttpPost]
    public async Task<IActionResult> Filter(int? departmentId, int? employeeId, DateTime? from, DateTime? to, int page = 1, int pageSize = 7)
    {
        var allFiltered = await _attendanceService.FilterAsync(departmentId, employeeId, from, to);
        var totalCount = allFiltered.Count();
        var items = allFiltered.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var pagedResult = new PageResult<Attendance>
        {
            items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        ViewBag.CurrentPage = page;
        ViewBag.DepartmentId = departmentId;
        ViewBag.EmployeeId = employeeId;
        ViewBag.From = from?.ToString("yyyy-MM-dd");
        ViewBag.To = to?.ToString("yyyy-MM-dd");

        return PartialView("AttendanceTable", pagedResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetStatus(int employeeId, DateTime date)
    {
        var record = await _attendanceService.GetByEmployeeAndDateAsync(employeeId, date.Date);
        if (record == null) return Json("NotMarked");
        return Json(record.Status.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> MarkAttendance(int employeeId, DateTime date, AttendanceStatus status)
    {
        try
        {
            var record = await _attendanceService.CreateOrUpdateAsync(employeeId, date, status);
            return Json(new { success = true, status = record.Status.ToString() });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesByDepartment(int departmentId)
    {
        var employees = await _employeeService.GetEmployees();
        var filtered = employees
            .Where(e => e.DepartmentId == departmentId)
            .Select(e => new { e.Id, e.FullName })
            .ToList();

        return Json(filtered);
    }


    private async Task PopulateFilters()
    {
        ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartments(), "Id", "Name");
        ViewBag.Employees = new SelectList(await _employeeService.GetEmployees(), "Id", "FullName");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var record = await _attendanceService.GetByIdAsync(id);
        if (record == null) return NotFound();

        var employees = await _employeeService.GetEmployees();
        ViewBag.Employees = new SelectList(employees, "Id", "FullName", record.EmployeeId);

        return View(record);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Attendance updated)
    {
        try
        {
            await _attendanceService.CreateOrUpdateAsync(updated.EmployeeId, updated.Date, updated.Status);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(updated);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var record = await _attendanceService.GetByIdAsync(id);
        if (record == null) return NotFound();
        return View(record);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _attendanceService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
