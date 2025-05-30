using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetCoreMVC_SchoolSystem.Services;
public class StudentService {
    SchoolDbContext _dbContext;
    public StudentService(SchoolDbContext dbcontext) {
        _dbContext = dbcontext;
        }
    public List<StudentDTO> GetAll() {
        var allStudents = _dbContext.Students.ToList();
        var studentDtos = new List<StudentDTO>();
        foreach (var student in allStudents) {
            StudentDTO studentDTO = ModelToDTO(student);
            studentDtos.Add(studentDTO);
            }
        return studentDtos;
        }
    internal async Task CreateAsync(StudentDTO newStudent) {
        Student studentToSave = DtoToModel(newStudent);
        await _dbContext.Students.AddAsync(studentToSave);
        await _dbContext.SaveChangesAsync();
        }

    internal async Task<StudentDTO> GetByIdAsync(int id) {
        var studentToEdit = await _dbContext.Students.FindAsync(id);
        return ModelToDTO(studentToEdit);
        }

    internal async Task UpdateAsync(StudentDTO studentDTO, int id) {
        _dbContext.Update(DtoToModel(studentDTO));
        await _dbContext.SaveChangesAsync();
        }
    internal async Task DeleteAsync(int id) {
        var studentToDelete = await _dbContext.Students.FindAsync(id);
        if (studentToDelete != null) {
            _dbContext.Students.Remove(studentToDelete);

            }
        await _dbContext.SaveChangesAsync();
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

    }

