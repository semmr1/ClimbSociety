using System.ComponentModel.DataAnnotations;

namespace ClimbSociety.Models;

public class DeveloperMessage
{
    
    [Required, EmailAddress, StringLength(100, MinimumLength = 1)]
    public string Email { get; set; }

    [Required, StringLength(300, MinimumLength = 1)]
    public string Subject { get; set; }
    
    [Required, StringLength(600, MinimumLength = 1)]
    public string MessageText { get; set; }
}