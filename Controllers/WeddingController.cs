using Microsoft.EntityFrameworkCore;
using weddingplanner.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
namespace weddingplanner.Controllers
{
  public class WeddingController : Controller
  {
    private Context dbContext;
    public WeddingController(Context context)
    {
      dbContext = context;
    }
    [HttpGet("Dashboard")]
    public IActionResult Success()
    {
      if (HttpContext.Session.GetInt32("uid") == null)
      {
        return RedirectToAction("Index", "Home");
      }
      List<Wedding> AllWeddings = dbContext.Weddings
      .Include(w => w.RSVP)
      .ThenInclude(we => we.User)
      .Include(wedding => wedding.Creater)
      .ToList();

      List<int> WeddingsToDelete = new List<int>();
      DateTime CurrentTime = DateTime.Now;
      foreach (var wedding in AllWeddings)
      {
        if (wedding.WeddingDate < CurrentTime)
        {
          WeddingsToDelete.Add(wedding.WeddingId);
        }
      }
      if (WeddingsToDelete.Count > 0)
      {
        foreach (var wedding in WeddingsToDelete)
        {
          Wedding WeddingToDelete = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == wedding);
          dbContext.Weddings.Remove(WeddingToDelete);
          dbContext.SaveChanges();
        }

      }
      int LoggedUser = (int)HttpContext.Session.GetInt32("uid");
      ViewBag.LoggedUser = LoggedUser;
      ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("uid"));

      int count = dbContext.Users
      .Include(u => u.Weddings)
      .ThenInclude(us => us.Wedding)
      .Where(use => use.Weddings.Any(user => user.UserId == LoggedUser))
      .Count();
      ViewBag.Count = count;
      return View(AllWeddings);
    }
    [HttpPost("Dashboard/new")]
    public IActionResult NewWedding(Wedding newWedding)
    {
      if (HttpContext.Session.GetInt32("uid") == null)
      {
        return RedirectToAction("Index", "Home");
      }
      if (ModelState.IsValid)
      {
        User LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("uid"));
        newWedding.Creater = LoggedUser;
        dbContext.Weddings.Add(newWedding);
        LoggedUser.WeddingsCreated.Add(newWedding);
        dbContext.SaveChanges();

        ViewBag.LoggedUser = (int)HttpContext.Session.GetInt32("uid");
        int weddingid = newWedding.WeddingId;
        return RedirectToAction("OneWeddingView", new { weddingid = weddingid });
      }
      else
      {
        if (newWedding.WeddingDate.Year == 1)
        {
          if (ModelState.ContainsKey("WeddingDate") == true)
          {
            ModelState["WeddingDate"].Errors.Clear();
          }
          ModelState.AddModelError("WeddingDate", "Invalid Date");
        }
        return View("NewWeddingView");
      }
    }
    [HttpGet("wedding/new")]
    public IActionResult NewWeddingView()
    {
      if (HttpContext.Session.GetInt32("uid") == null)
      {
        return RedirectToAction("Index", "Home");
      }
      return View();
    }

    [HttpGet("wedding/{weddingid}")]
    public IActionResult OneWeddingView(int weddingid)
    {
      if (HttpContext.Session.GetInt32("uid") == null)
      {
        return RedirectToAction("Index", "Home");
      }
      Wedding OneWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);

      List<User> RSVPUsers = dbContext.Users
      .Include(u => u.Weddings)
      .ThenInclude(u => u.Wedding)
      .Where(wed => wed.Weddings.Any(wedd => wedd.WeddingId == weddingid))
      .ToList();

      ViewBag.RSVPUsers = RSVPUsers;
      return View(OneWedding);
    }

    [HttpPost("wedding/delete/{weddingid}")]
    public IActionResult DeleteWedding(int weddingid)
    {
      if (HttpContext.Session.GetInt32("uid") == null)
      {
        return RedirectToAction("Index", "Home");
      }
      Wedding WeddingToDelete = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
      dbContext.Weddings.Remove(WeddingToDelete);
      dbContext.SaveChanges();
      return RedirectToAction("Success");
    }

    [HttpPost("wedding/rsvp/{weddingid}")]
    public IActionResult RSVPWedding(int weddingid)
    {
      if (HttpContext.Session.GetInt32("uid") == null)
      {
        return RedirectToAction("Index", "Home");
      }
      Wedding WeddingToRSVP = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
      int uid = (int)HttpContext.Session.GetInt32("uid");
      User LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == uid);
      UserWedding newUserWedding = new UserWedding
      {
        WeddingId = weddingid,
        Wedding = WeddingToRSVP,
        UserId = uid,
        User = LoggedUser,
      };
      dbContext.UsersWeddings.Add(newUserWedding);
      dbContext.SaveChanges();
      return RedirectToAction("Success");
    }

    [HttpPost("wedding/unrsvp/{weddingid}")]
    public IActionResult UnRSVPWedding(int weddingid)
    {
      if (HttpContext.Session.GetInt32("uid") == null)
      {
        return RedirectToAction("Index", "Home");
      }
      Wedding WeddingToRSVP = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
      int uid = (int)HttpContext.Session.GetInt32("uid");
      User LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == uid);

      UserWedding UserWeddingToDelete = dbContext.UsersWeddings.FirstOrDefault(uw => uw.WeddingId == weddingid && uw.UserId == uid);
      dbContext.UsersWeddings.Remove(UserWeddingToDelete);
      dbContext.SaveChanges();
      return RedirectToAction("Success");
    }
  }
}