using Microsoft.AspNetCore.Mvc;

namespace PosterCMS.Controllers
{
    public class PosterController : Controller
    {
        // GET: PosterController
        public ActionResult Index()
        {
            return View();
        }

    }
}
