using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Backend.Models;

namespace Backend.Controllers;


  public class TrackerController : Controller {
    // Get /
    public IActionResult Index()
    {
      return View();
    }

    // Get /Welcome hello world

    public IActionResult Welcome(string name)
    {
      ViewData["name"] = name;
      return View();
      // return HtmlEncoder.Default.Encode($"Hello {name}, Welcome.");
    }

  }

