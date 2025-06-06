using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Models;
using AspNetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC_SchoolSystem.Controllers;
[Authorize]
public class SubjectsController : Controller {
    SubjectService _subjectService;

    public SubjectsController(SubjectService subjectService) {
        _subjectService = subjectService;
        }
    [HttpGet]
    public IActionResult Index() {
        var allSubjects = _subjectService.GetAll();
        return View(allSubjects);
        }
    [HttpGet]
    [Authorize(Roles = "Chief, Admin")]
    public IActionResult Create() {
        return View();
        }
    [HttpPost]
    [Authorize(Roles = "Chief, Admin")]
    public async Task<IActionResult> CreateAsync(SubjectDTO newSubject) {
        if (!ModelState.IsValid) {
            return View(newSubject);
            }
        await _subjectService.CreateAsync(newSubject);
        return RedirectToAction("Index");
        }
    [HttpGet]
    [Authorize(Roles = "Teacher, Admin")]
    public async Task<IActionResult> EditAsync(int id) {
        var subjecttToEdit = await _subjectService.GetByIdAsync(id);
        if (subjecttToEdit == null) {return View("NotFound");}
        return View(subjecttToEdit);
        }
    [HttpPost]
    [Authorize(Roles = "Teacher, Admin")]
    public async Task<IActionResult> EditAsync(SubjectDTO subject, int id) {
        await _subjectService.UpdateAsync(subject, id);
        return RedirectToAction("Index");
        }
    [HttpPost]
    [Authorize(Roles = "Chief, Admin")]
    public async Task<IActionResult> DeleteAsync(int id) {
        await _subjectService.DeleteAsync(id);
        return RedirectToAction("Index");
        }
    }
    

