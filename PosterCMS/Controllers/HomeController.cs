using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PosterCMS.Models;

namespace PosterCMS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly PosterDbContext _context;

    public HomeController(ILogger<HomeController> logger, PosterDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var Posters = _context.Posters.ToList();
        return View(Posters);
    }

    public IActionResult Account()
    {
        return View();
    }

    public IActionResult SignIn()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
