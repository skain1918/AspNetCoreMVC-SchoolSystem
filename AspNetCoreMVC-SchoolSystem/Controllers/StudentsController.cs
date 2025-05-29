using AspNetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC_SchoolSystem.Controllers {
    public class StudentsController : Controller {
        StudentService _studentService;
        public StudentsController(StudentService studentService) {
            _studentService = studentService;
            }
        public IActionResult Index() {
            var allStudents = _studentService.GetAll();
            return View(allStudents);
            }
        }
    }
