using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

public class Message
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, EmailAddress, StringLength(100, MinimumLength = 1)]
    public string Email { get; set; }

    [Required, StringLength(300, MinimumLength = 1)]
    public string Subject { get; set; }

    [Required, StringLength(600, MinimumLength = 1)]
    public string MessageText { get; set; }
}