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
        public IActionResult Edit(int id) {
            var studentToEdit = _studentService.GetByIdAsync(id);
            return View(studentToEdit);
            }
        }
    }
