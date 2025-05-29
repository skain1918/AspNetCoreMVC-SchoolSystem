using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Threading.Tasks;

namespace AspNetCoreMVC_SchoolSystem.Services;
public class StudentService {
    SchoolDbContext _dbcontext;
    public StudentService(SchoolDbContext dbcontext) {
        _dbcontext = dbcontext;
        }
    public List<StudentDTO> GetAll() {
        var allStudents = _dbcontext.Students.ToList();
        var studentDtos = new List<StudentDTO>();
        foreach (var student in allStudents) {
            StudentDTO studentDTO = ModelToDTO(student);
            studentDtos.Add(studentDTO);
            }
        return studentDtos;
        }
    internal async Task CreateAsync(StudentDTO newStudent) {
        Student studentToSave = new Student
            {
            DateOfBirth = newStudent.DateOfBirth,
            FirstName = newStudent.FirstName,
            LastName = newStudent.LastName,
            Id = newStudent.Id
            };
        await _dbcontext.Students.AddAsync(studentToSave);
        await _dbcontext.SaveChangesAsync();
        }

    internal async Task<StudentDTO> GetByIdAsync(int id) {
        var studentToEdit = await _dbcontext.Students.FindAsync(id);
        return ModelToDTO(studentToEdit);
        }
    private StudentDTO ModelToDTO(Student student) {
        return new StudentDTO() {
            DateOfBirth = student.DateOfBirth,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Id = student.Id
            };
        }
    }

