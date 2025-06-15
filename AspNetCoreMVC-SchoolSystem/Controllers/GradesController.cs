using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Services;
using AspNetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreMVC_SchoolSystem.Controllers;
[Authorize]
public class GradesController : Controller {
    GradeService _gradeService;

    public GradesController(GradeService gradeService) {
        _gradeService = gradeService;
        }
    [HttpGet]
    public IActionResult Index() {
        IEnumerable<GradeDTO> allGrades = _gradeService.GetAll();
        return View(allGrades);
        }
    public IActionResult MyGrades() {
        IEnumerable<GradeDTO> myGrades = _gradeService.GetAll()
            .Where(grade => grade.StudentId.ToString() == User.FindFirst("StudentId")?.Value);
        return View(myGrades);
        }
    [HttpGet]
    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult Create() {
        FillDropDowns();
        return View();
        }

    [HttpPost]
    [Authorize(Roles = "Teacher, Admin")]
    public async Task<IActionResult> CreateAsync(GradeDTO newGrade) {
        await _gradeService.CreateAsync(newGrade);
        return RedirectToAction("Index");
        }
    [HttpGet]
    [Authorize(Roles = "Teacher, Admin")]
    public async Task<IActionResult> EditAsync(int id, GradeDTO gradeDTO) {
        GradeDTO gradeToEdit = await _gradeService.FindByIdAsync(id);
        if (gradeToEdit == null) {
            return View("NotFound");
            }
        FillDropDowns();
        return View(gradeToEdit);
        }
    [HttpPost]
    [Authorize(Roles = "Teacher, Admin")]
    public async Task<IActionResult> EditAsync(GradeDTO updatedGrade) {
        await _gradeService.UpdateAsync(updatedGrade);
        return RedirectToAction("Index");
        }
    [HttpPost]
    [Authorize(Roles = "Teacher, Admin")]
    public async Task<IActionResult> DeleteAsync(int id) {
        GradeDTO gradeToDelete = await _gradeService.FindByIdAsync(id);
        if (gradeToDelete == null) {
            return View("NotFound");
            }
        await _gradeService.DeleteAsync(id);
        return RedirectToAction("Index");
        }
    private void FillDropDowns() {
        GradesDropdownsViewModel gradesDropdownsData = _gradeService.GetGradesDropdownsData();
        ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "FullName");
        ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
        }

    }
