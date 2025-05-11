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

        public IActionResult CreatePoster(PosterModel poster){
            _context.Add(poster);
            _context.SaveChanges();

            return RedirectToAction("Index", poster);
        }

        public IActionResult EditPoster(PosterModel poster){
            _context.Update(poster);
            _context.SaveChanges();

            return RedirectToAction("Index", poster);
        }

        public IActionResult DeletePoster(int id){
            var toDelete = _context.Posters.SingleOrDefault(x => x.ID == id);
            _context.Posters.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
