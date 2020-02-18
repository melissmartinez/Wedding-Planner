using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace weddingplanner.Models
{
  [NotMapped]
  public class UserLogin
  {
    [Required(ErrorMessage = "Error. Email Address is required")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string LoginEmail {get;set;}

    [Required(ErrorMessage = "Error. Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [MinLength(7, ErrorMessage = "Error. Password must be at least 7 characters")]
    public string LoginPassword {get;set;}
  }

}