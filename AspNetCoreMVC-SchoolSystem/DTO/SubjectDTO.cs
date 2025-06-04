using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMVC_SchoolSystem.Models {
    public class SubjectDTO {
        public int Id { get; set; }
        //[MinLength(3), MaxLength(50)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters long.")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        }
    }
