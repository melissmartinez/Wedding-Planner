using System.ComponentModel.DataAnnotations;

namespace weddingplanner.Models
{
    public class UserWedding
    {
      [Key]
      public int UserWeddingId {get;set;}
      public int UserId {get;set;}
      public int WeddingId {get;set;}

      public User User {get;set;}
      public Wedding Wedding {get;set;}
    }
}