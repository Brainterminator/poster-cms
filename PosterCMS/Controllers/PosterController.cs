using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PosterCMS.Models;

namespace PosterCMS.Controllers
{
    public class PosterController : Controller
    {
        // GET: PosterController

        private readonly PosterDbContext _context;

        public PosterController(PosterDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var model = _context.Posters.Find(id);
            return View("Index", model);
        }

        public IActionResult Edit(int id)
        {
            var model = _context.Posters.Find(id);
            ViewData["FormAction"] = "EditPoster";
            return View("Editor", model);
        }

        public IActionResult Create()
        {
            ViewData["FormAction"] = "CreatePoster";
            return View("Editor", new PosterModel());
        }

        public async Task<IActionResult> CreatePoster(PosterModel poster)
        {
            poster.CreateDate = DateTime.UtcNow;
            poster.EditDate = DateTime.UtcNow;

            var email = Request.Cookies["AuthEmail"];
            if (email != null)
            {
                poster.Author = email;
            }

            _context.Add(poster);
            _context.SaveChanges();

            ContentManager.SaveImage(await ContentManager.GenerateA4Thumbnail("http://localhost:5299/Poster/Index/" + poster.ID), "thumbnails/" + poster.ID + ".jpg");

            return RedirectToAction("Index", new { id = poster.ID });
        }

        public async Task<IActionResult> EditPoster(PosterModel poster)
        {
            poster.EditDate = DateTime.UtcNow;
            poster.CreateDate = DateTime.SpecifyKind(poster.CreateDate, DateTimeKind.Utc);

            var email = Request.Cookies["AuthEmail"];
            if (email != null)
            {
                poster.Author = email;
            }

            _context.Update(poster);
            _context.SaveChanges();

            ContentManager.SaveImage(await ContentManager.GenerateA4Thumbnail("http://localhost:5299/Poster/Index/" + poster.ID), "thumbnails/" + poster.ID + ".jpg");

            return RedirectToAction("Index", new { id = poster.ID });
        }

        public IActionResult DeletePoster(int id)
        {
            var toDelete = _context.Posters.SingleOrDefault(x => x.ID == id);
            if (toDelete != null)
            {
                _context.Posters.Remove(toDelete);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ExportPoster(PosterModel poster)
        {
            var stream = await ContentManager.GeneratePDF(poster);
            return File(stream, "application/pdf", "poster-printout.pdf");
        }
    }
}
