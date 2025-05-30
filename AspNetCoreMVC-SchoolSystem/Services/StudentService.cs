using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
        Student studentToSave = DtoToModel(newStudent);
        await _dbcontext.Students.AddAsync(studentToSave);
        await _dbcontext.SaveChangesAsync();
        }

    internal async Task<StudentDTO> GetByIdAsync(int id) {
        var studentToEdit = await _dbcontext.Students.FindAsync(id);
        return ModelToDTO(studentToEdit);
        }

    internal async Task UpdateAsync(StudentDTO studentDTO, int id) {
        _dbcontext.Update(DtoToModel(studentDTO));
        await _dbcontext.SaveChangesAsync();
        }

    private StudentDTO ModelToDTO(Student student) {
        return new StudentDTO()
            {
            DateOfBirth = student.DateOfBirth,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Id = student.Id
            };
        }
    private Student DtoToModel(StudentDTO newStudent) {
        return new Student
            {
            DateOfBirth = newStudent.DateOfBirth,
            FirstName = newStudent.FirstName,
            LastName = newStudent.LastName,
            Id = newStudent.Id
            };
        }

    internal async Task DeleteAsync(int id) {
        var studentToDelete = await _dbcontext.Students.FindAsync(id);
        if (studentToDelete != null) {
            _dbcontext.Students.Remove(studentToDelete);

            }
        await _dbcontext.SaveChangesAsync();    
            }
    }

