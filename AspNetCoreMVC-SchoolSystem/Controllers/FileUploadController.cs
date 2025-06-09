using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;

namespace AspNetCoreMVC_SchoolSystem.Controllers;

public class FileUploadController : Controller {
    StudentService _studentService;
    public FileUploadController(StudentService studentService) {
        _studentService = studentService;
        }
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file) {
        string filePath = Path.GetFullPath(file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create)) {
            await file.CopyToAsync(stream);
            stream.Close();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlElement root = xmlDoc.DocumentElement;
            //foreach (XmlNode node in root.SelectNodes("/Students/Student")) {
            //    StudentDTO studentDto = new StudentDTO
            //        {
            //        FirstName = node.ChildNodes[0].InnerText,
            //        LastName = node.ChildNodes[1].InnerText,
            //        DateOfBirth = DateOnly.Parse(node.ChildNodes[2].InnerText, CultureInfo.CreateSpecificCulture("cs-CZ"))
            //        };
            //    await _studentService.CreateAsync(studentDto);
            //    }
            foreach (XmlNode node in root.SelectNodes("/Students/Student")) {
                StudentDTO studentDto = new StudentDTO
                    {
                    FirstName = node.SelectSingleNode("FirstName")?.InnerText,
                    LastName = node.SelectSingleNode("LastName")?.InnerText,
                    DateOfBirth = DateOnly.Parse(
                        node.SelectSingleNode("DateOfBirth")?.InnerText ?? string.Empty,
                        CultureInfo.CreateSpecificCulture("cs-CZ"))
                    };
                await _studentService.CreateAsync(studentDto);
                }
            return RedirectToAction("Index", "Students");
            }
        }
    }