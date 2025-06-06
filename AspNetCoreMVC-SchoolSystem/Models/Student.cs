using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreMVC_SchoolSystem.Models;
public class Student {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    [NotMapped]
    public string FullName {
        get {
            return $"{FirstName} {LastName}";
            }
        }
    }

