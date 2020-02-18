using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace weddingplanner.Models
{
    public class Wedding
  {
    [Key]
    public int WeddingId {get;set;}

    [Required(ErrorMessage="Wedder One field is required.")]
    [MinLength(2)]
    [Display(Name = "Wedder One")]
    public string WedderOne {get;set;}

    [Required(ErrorMessage="Wedder Two field is required.")]
    [MinLength(2)]
    [Display(Name = "Wedder Two")]
    public string WedderTwo {get;set;}

    [Required(ErrorMessage="Wedding Date field is required.")]
    [DataType(DataType.Date)]
    [Display(Name = "Date")]
    public DateTime WeddingDate {get;set;}

    [Required(ErrorMessage = "Address field is required.")]
    [Display(Name = "Wedding Address")]
    public string Address{get;set;}

    public User Creater {get;set;}
    public List<UserWedding> RSVP {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
  }
}