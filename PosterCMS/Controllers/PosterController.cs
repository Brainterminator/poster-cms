using System.Diagnostics.Tracing;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = _context.Posters.Find(id);
            ViewData["FormAction"] = "EditPoster";
            return View("Editor", id);
        }

        public IActionResult Create()
        {
            ViewData["FormAction"] = "CreatePoster";
            return View("Editor", new PosterModel());
        }

        public IActionResult CreatePoster(PosterModel poster){
            _context.Add(poster);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditPoster(PosterModel poster){
            return RedirectToAction("Index");
        }
    }
}
