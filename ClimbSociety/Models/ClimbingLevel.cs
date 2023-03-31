using System.ComponentModel.DataAnnotations;

namespace ClimbSociety.Models
{
    public class ClimbingLevel
    {
        [Key]
        [Required(ErrorMessage = "Value must be a valid climbing level")]
        [RegularExpression(@" ([0 - 9][A - C])")]
        public string Level { get; set; }
    }
}
