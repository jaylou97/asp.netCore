using System.ComponentModel.DataAnnotations;

namespace CsharpSampleWeb.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is Required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required.")]
        public string? LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Contact number is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Please enter a 11-digit contact number.")]
        public string? ContactNo { get; set; }

    }
    public class MyCombinedModel
    {
        public IEnumerable<CsharpSampleWeb.Models.Person> People { get; set; }
        public CsharpSampleWeb.Models.Person SinglePerson { get; set; }
    }

}
