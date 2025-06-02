using AspNetCoreMVC_SchoolSystem.Models;

namespace AspNetCoreMVC_SchoolSystem.DTO {
    public class GradeDTO {
            public int Id { get; set; }
            public int StudentId { get; set; }
            public int SubjectId { get; set; }
            public string Topic { get; set; }
            public int Mark { get; set; }
            public DateTime Date { get; set; }
            }
    }
