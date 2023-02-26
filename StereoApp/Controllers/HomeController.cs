using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StereoApp.Models;

namespace StereoApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Home()
    {
        return View();
    }

    public IActionResult Towers()
    {
        return View();
    }

    public IActionResult Bookshelves()
    {
        return View();
    }

    public IActionResult Subwoofers()
    {
        return View();
    }

    public IActionResult Amplifiers()
    {
        return View();
    }

    public IActionResult CDPlayers()
    {
        return View();
    }   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

