﻿using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Models;
using AspNetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMVC_SchoolSystem.Services;
public class GradeService {
    SchoolDbContext _dbContext;
    public GradeService(SchoolDbContext dbContext) {
        _dbContext = dbContext;
        }

    internal async Task CreateAsync(GradeDTO newGrade) {
        Grade gradeToInsert = await DtoToModelAsync(newGrade);
        await _dbContext.Grades.AddAsync(gradeToInsert);
        await _dbContext.SaveChangesAsync();
        }

    private async Task<Grade> DtoToModelAsync(GradeDTO newGrade) {
        return new Grade()
            {
            Mark = newGrade.Mark,
            Topic = newGrade.Topic,
            Student = await _dbContext.Students.FindAsync(newGrade.StudentId),
            Subject = await _dbContext.Subjects.FindAsync(newGrade.SubjectId),
            Date = DateTime.Now,
            Id = newGrade.Id
            };
        }
    internal async Task<GradeDTO> FindByIdAsync(int id) {
        var gradeToReturn = await _dbContext.Grades.Include(gr => gr.Subject).Include(gr => gr.Student).FirstOrDefaultAsync(gr => gr.Id == id);
        if (gradeToReturn == null) { return null; }
        return ModelToDto(gradeToReturn);
        }
    internal IEnumerable<GradeDTO> GetAll() {

        var allGrades = _dbContext.Grades.Include(gr => gr.Student).Include(gr => gr.Subject);
        List<GradeDTO> gradeDTOs = new List<GradeDTO>();
        foreach (var grade in allGrades) {
            gradeDTOs.Add(ModelToDto(grade));
            }
        return gradeDTOs;
        }
    private GradeDTO ModelToDto(Grade grade) {
        return (new GradeDTO()
            {
            Id = grade.Id,
            Mark = grade.Mark,
            Topic = grade.Topic,
            StudentId = grade.Student.Id,
            SubjectId = grade.Subject.Id,
            Date = grade.Date,
            StudentFullName = $"{grade.Student.FirstName} {grade.Student.LastName}",
            SubjectName = grade.Subject.Name
            });
        }
    internal GradesDropdownsViewModel GetGradesDropdownsData() {
        return new GradesDropdownsViewModel()
            {
            Students = _dbContext.Students.OrderBy(student => student.LastName),
            Subjects = _dbContext.Subjects.OrderBy(subject => subject.Name)
            };
        }
    internal async Task UpdateAsync(GradeDTO updatedGrade) {
        Grade gradeToSave = await DtoToModelAsync(updatedGrade);
        _dbContext.Grades.Update(gradeToSave);
        await _dbContext.SaveChangesAsync();
        }

    internal async Task DeleteAsync(int Id) {
        Grade gradeToDelete = await _dbContext.Grades.FindAsync(Id);
        _dbContext.Grades.Remove(gradeToDelete);
        await _dbContext.SaveChangesAsync();
        }
    }