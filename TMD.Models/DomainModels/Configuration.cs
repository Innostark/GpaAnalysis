using System.ComponentModel.DataAnnotations;

namespace TMD.Models.DomainModels
{
    public class Configuration
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Name is required")]
        [StringLength(50, ErrorMessage = "Name must be between 1 and 50 characters in length.")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [StringLength(50, ErrorMessage = "Type must be between 1 and 50 characters in length.")]
        public string Type { get; set; }

        [StringLength(1024, ErrorMessage = "Value must be between 1 and 1024 characters in length (NULL is allowed).")]
        public string Value { get; set; } 
    }
}
