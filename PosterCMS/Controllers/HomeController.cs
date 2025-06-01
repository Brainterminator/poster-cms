using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    public async Task<IActionResult> Index()
    {
        var Posters = _context.Posters.OrderByDescending(p => p.EditDate).ToList();
        var Previews = new Dictionary<int, byte[]>();

        foreach (var poster in Posters)
        {
            Previews.Add(poster.ID, await ImageGen.GenerateA4Thumbnail("http://localhost:5299/Poster/Index/" + poster.ID));
        }

        ViewBag.Previews = Previews;
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
