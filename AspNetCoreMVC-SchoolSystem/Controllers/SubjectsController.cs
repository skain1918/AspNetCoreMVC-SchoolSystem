using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Models;
using AspNetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC_SchoolSystem.Controllers; 
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
    public IActionResult Create() {
        return View();
        }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(SubjectDTO newSubject) {
        await _subjectService.CreateAsync(newSubject);
        return RedirectToAction("Index");
        }
    [HttpGet]
    public async Task<IActionResult> EditAsync(int id) {
        var subjecttToEdit = await _subjectService.GetByIdAsync(id);
        return View(subjecttToEdit);
        }
    [HttpPost]
    public async Task<IActionResult> EditAsync(SubjectDTO subject, int id) {
        await _subjectService.UpdateAsync(subject, id);
        return RedirectToAction("Index");
        }
    [HttpPost]
    public async Task<IActionResult> DeleteAsync(int id) {
        await _subjectService.DeleteAsync(id);
        return RedirectToAction("Index");
        }
    }
    

