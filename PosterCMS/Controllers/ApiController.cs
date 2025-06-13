using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PosterCMS.Models;

namespace PosterCMS.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly PosterDbContext _context;

        public ApiController(PosterDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<PosterModel> GetPoster(int id)
        {
            var poster = _context.Posters.Find(id);

            if (poster == null)
            {
                return NotFound();
            }

            return poster;
        }
    }
}
