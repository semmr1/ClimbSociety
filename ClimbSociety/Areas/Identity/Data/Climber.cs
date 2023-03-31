using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ClimbSociety.Models;
using Microsoft.AspNetCore.Identity;

namespace ClimbSociety.Areas.Identity.Data;

public class Climber : IdentityUser
{
    [PersonalData]
    [Required(ErrorMessage = "Please fill in a first name")]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
    public string FirstName { get; set; }

    [PersonalData]
    [Required(ErrorMessage = "Please fill in a last name")]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
    public string LastName { get; set; }

    [PersonalData]
    [Required(ErrorMessage = "Please select a date")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Please select a climbing level")]
    [ForeignKey("Level")]
    //[RegularExpression(@" ([0 - 9][A - C])")]
    public string ClimbingLevel { get; set; }
}

