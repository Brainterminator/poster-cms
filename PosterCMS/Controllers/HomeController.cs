using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
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
        var auth = Request.Cookies["AuthEmail"];
        UserModel? model = _context.Users.Find(auth);
        if (model == null)
        {
            return RedirectToAction("SignIn");
        }
        else return View(model);
    }

    public IActionResult SignIn()
    {
        return View();
    }

    public IActionResult Login(LoginModel model)
    {
        var user = _context.Users.Find(model.Email);
        if (user != null)
        {
            if (PasswordHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                HttpContext.Response.Cookies.Append("AuthEmail", user.Email, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return RedirectToAction("Index");
            }

            ViewBag.Message = "Invalid login.";
        }
        else ViewBag.Message = "Unknown user.";
        return View("SignIn");
    }

    public IActionResult Register(LoginModel model)
    {
        var user = _context.Users.Find(model.Email);
        if (user == null)
        {
            byte[] hash, salt;
            PasswordHelper.CreatePasswordHash(model.Password, out hash, out salt);
            user = new UserModel
            {
                Email = model.Email,
                PasswordHash = hash,
                PasswordSalt = salt
            };
            _context.Add(user);
            _context.SaveChanges();
            {
                HttpContext.Response.Cookies.Append("AuthEmail", user.Email, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return RedirectToAction("Account");
            }
        }
        ViewBag.Message = "Invalid User.";
        return RedirectToAction("SignIn");
    }

    public IActionResult LogOut()
    {
        {
            HttpContext.Response.Cookies.Delete("AuthEmail", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index");
        }
    }

    public IActionResult EditAccount(UserModel model)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
        if (user == null)
        {
            return RedirectToAction("SignIn");
        }
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Socials = model.Socials;
        _context.Update(user);
        _context.SaveChanges();

        return RedirectToAction("Account");
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
            return View("ImageUploader");
        }

        return RedirectToAction("ViewImages");
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

    [HttpGet]
    public IActionResult DeleteImage(String path)
    {
        ContentManager.DeleteImage(path);
        return RedirectToAction("ViewImages");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
