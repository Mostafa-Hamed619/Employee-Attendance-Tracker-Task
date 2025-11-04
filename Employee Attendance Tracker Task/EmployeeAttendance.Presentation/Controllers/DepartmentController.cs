using EmployeeAttendance.Business.Interfaces;
using EmployeeAttendance.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAttendance.Presentation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _deptService;

        public DepartmentController(IDepartmentService deptService)
        {
            _deptService = deptService;
        }

        public async Task<IActionResult> Index()
        {
            var Departments = await _deptService.GetAllDepartments();

            return View(Departments);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            var isCodeUnique = await _deptService.IsCodeUnique(department.Id, department.Code);
            var isNameUnique = await _deptService.IsNameUnique(department.Id, department.Name);

            if (!isCodeUnique)
            {
                TempData["Error"] = "The code is already existing";
                return RedirectToAction("Create");
            }
            if (!isNameUnique)
            {
                TempData["Error"] = "The Name is already existing";
                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _deptService.CreateAsync(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(department);
        }


        public async Task<IActionResult> Edit(int Id)
        {
            var department = await _deptService.GetDepartmentById(Id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, Department department)
        {
            if (Id != department.Id)
                return BadRequest();

            var isCodeUnique = await _deptService.IsCodeUnique(department.Id, department.Code);
            var isNameUnique = await _deptService.IsNameUnique(department.Id, department.Name);

            if (!isCodeUnique)
            {
                TempData["Error"] = "The code is already existing";
                return View(department);
            }
            if (!isNameUnique)
            {
                TempData["Error"] = "The Name is already existing";
                return View(department);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _deptService.UpdateAsync(department);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await _deptService.GetDepartmentById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            await _deptService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
