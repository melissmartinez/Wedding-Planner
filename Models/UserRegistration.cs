using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace weddingplanner.Models
{
    [NotMapped]
  public class UserRegistration
  {
    [Required(ErrorMessage = "Error. First Name is required")]
    [MinLength(3, ErrorMessage = "Error. First Name must be at least 8 characters long.")]
    [Display(Name = "First Name")]
    public string FirstName {get;set;}

    [Required(ErrorMessage = "Error. Last Name is required")]
    [MinLength(3, ErrorMessage = "Last Name must be at least 8 characters long.")]
    [Display(Name = "Last Name")]
    public string LastName {get;set;}

    [Required(ErrorMessage = "Error. Email Address is required")]
    [EmailAddress]
    public string Email {get;set;}

    [Required(ErrorMessage = "Error. Password is required")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    [MinLength(7, ErrorMessage = "Error. Password must be at least 8 characters long.")]
    public string Password {get;set;}

    [NotMapped]
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword {get;set;}
  }
}