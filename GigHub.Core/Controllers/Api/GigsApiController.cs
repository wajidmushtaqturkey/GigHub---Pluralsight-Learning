using GigHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace GigHub.Core.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GigsApiController : ControllerBase
    {
        private ApplicationDbContext _context;

        public GigsApiController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpDelete("cancel/{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            /* 
             * make sure the user deleting a gig is the registered user 
             * that created it
             * */
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.Cancel();

            _context.SaveChanges();

            return Ok();
        }
    }
}
