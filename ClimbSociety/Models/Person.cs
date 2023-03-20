using System.ComponentModel.DataAnnotations;

namespace ClimbSociety.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Please fill in a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please fill in a last name")]
        public string LastName { get; set; }

        [Range(1900, 2006, ErrorMessage = "Value must be between 1900 and 2006")]
        public int YearOfBirth { get; set; }
    }
}
