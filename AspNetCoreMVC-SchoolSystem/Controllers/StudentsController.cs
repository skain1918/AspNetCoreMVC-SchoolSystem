using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC_SchoolSystem.Controllers {
    public class StudentsController : Controller {
        StudentService _studentService;
        public StudentsController(StudentService studentService) {
            _studentService = studentService;
            }
        [HttpGet]
        public IActionResult Index() {
            var allStudents = _studentService.GetAll();
            return View(allStudents);
            }
        [HttpGet]
        public IActionResult Create() {
            return View();
            }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentDTO newStudent) {
            await _studentService.CreateAsync(newStudent);
            return RedirectToAction("Index");
            }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var studentToEdit = await _studentService.GetByIdAsync(id);
            return View(studentToEdit);
            }
        [HttpPost]
        public async Task<IActionResult> EditAsync(StudentDTO studentDTO, int id) {
            await _studentService.UpdateAsync(studentDTO, id);
            return RedirectToAction("Index");
            }
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _studentService.DeleteAsync(id);
            return RedirectToAction("Index");
            }
        [HttpGet]
        public IActionResult Search(string q) {
            var foundStudents = _studentService.GetByName(q);
            return View("Index", foundStudents);
            }
        public async Task<IActionResult> GetToDelete(int id) {
            var studentDetails = await _studentService.GetByIdAsync(id);
            return View(studentDetails);
            }
        }
    }
