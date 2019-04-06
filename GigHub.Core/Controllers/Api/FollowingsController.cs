using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web.Http;
using System.Security.Claims;

namespace GigHub.Core.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FollowingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("follow")]
        public IActionResult Follow([FromBody] FollowingDto dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (_context.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
                    return BadRequest("Following already exists");

                var following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = dto.FolloweeId
                };

                _context.Followings.Add(following);
                _context.SaveChanges();

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
