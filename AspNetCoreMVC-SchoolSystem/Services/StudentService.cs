using AspNetCoreMVC_SchoolSystem.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

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
            StudentDTO studentDTO = new StudentDTO() {
                DateOfBirth = student.DateOfBirth,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Id = student.Id
                };
            studentDtos.Add(studentDTO);
            }
        return studentDtos;
        }
    }

