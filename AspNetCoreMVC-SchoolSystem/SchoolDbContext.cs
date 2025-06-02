using AspNetCoreMVC_SchoolSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMVC_SchoolSystem;

public class SchoolDbContext : DbContext{
    public SchoolDbContext(DbContextOptions<SchoolDbContext>options):base(options){    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Grade> Grades { get; set; }
    }
