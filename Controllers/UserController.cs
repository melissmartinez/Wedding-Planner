using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using weddingplanner.Models;

namespace weddingplanner.Controllers
{
  public class UserController : Controller
  {
    private Context dbContext;
    public UserController(Context context)
    {
      dbContext = context;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
      return View();
    }
    [HttpPost("user/register")]
    public IActionResult Register(UserRegistration newUser)
    {
      if (ModelState.IsValid)
      {
        if (dbContext.Users.Any(u => u.Email == newUser.Email))
        {
          ModelState.AddModelError("Email", "Email already in use!");
          return View("Index");
        }
        else
        {
          PasswordHasher<UserRegistration> Hasher = new PasswordHasher<UserRegistration>();
          newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
          User NewUser = new User
          {
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
            Password = newUser.Password,
          };
          dbContext.Users.Add(NewUser);
          dbContext.SaveChanges();

          int uid = NewUser.UserId;
          HttpContext.Session.SetInt32("uid", uid);

          return RedirectToAction("Success", "Wedding");
        }
      }
      else
      {
        return View("Index");
      }
    }
    [HttpPost("user/login")]
    public IActionResult Login(UserLogin currentUser)
    {
      if (ModelState.IsValid)
      {
        User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == currentUser.LoginEmail);
        if (userInDb == null)
        {

          ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
          return View("Index");
        }
       
        var hasher = new PasswordHasher<UserLogin>();

        var result = hasher.VerifyHashedPassword(currentUser, userInDb.Password, currentUser.LoginPassword);

        if (result == 0)
        {
          ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
          return View("Index");
        }
        int uid = userInDb.UserId;
        HttpContext.Session.SetInt32("uid", uid);

        return RedirectToAction("Success", "Wedding");
      }
      else
      {
        return View("Index");
      }
    }

    [HttpGet("user/logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Index");
    }
  }
}