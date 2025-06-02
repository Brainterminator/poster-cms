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

    private readonly IWebHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, PosterDbContext context, IWebHostEnvironment env)
    {
        _logger = logger;
        _context = context;
        _env = env;
    }

    public IActionResult Index()
    {
        var Posters = _context.Posters.OrderByDescending(p => p.EditDate).ToList();
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

    public IActionResult ImageUploader(ImageModel? model)
    {
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(ImageModel model)
    {
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

            // Optional: Save file name/path to DB here

            ViewBag.Message = "Image uploaded successfully!";
        }
        else
        {
            ViewBag.Message = "No file selected.";
        }

        return View("ImageUploader");
    }

    [HttpGet]
    public IActionResult ViewImages()
    {
        var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
        var imageFiles = Directory.GetFiles(uploadsPath)
            .Select(Path.GetFileName)
            .ToList();

        return View(imageFiles);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
