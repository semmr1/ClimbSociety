using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbSociety.Models
{
    public class Match
    {
        
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [ForeignKey("ClimberId")]
        public string YourId { get; set; }
        public string PartnerId { get; set; }
    }
}
