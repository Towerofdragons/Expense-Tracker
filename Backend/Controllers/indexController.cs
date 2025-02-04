using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Backend.Models;

namespace Backend.Controllers;


  public class TrackerController : Controller {
    // Get /
    public string Index()
    {
      return "Hello World";
    }

    // Get /Welcome hello world

    public string Welcome(string name){
      return HtmlEncoder.Default.Encode($"Hello {name}, Welcome.");

    }

  }

