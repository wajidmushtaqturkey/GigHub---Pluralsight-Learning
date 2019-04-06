using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace GigHub.Core.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("attend")]
        public IActionResult Attend([FromBody] AttendanceDto dto)
        {
            try
            {
                // Get user Id
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Check if gig exists
                if (_context.Gigs.FirstOrDefault(g => g.Id == dto.GigId) == null)
                {
                    return BadRequest($"The gig with id: {dto.GigId} doesn't exist");
                }

                // Check if user exists
                if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
                    return BadRequest("The attendance already exists.");

                var attendance = new Attendance
                {
                    GigId = dto.GigId,
                    AttendeeId = userId
                };

                _context.Attendances.Add(attendance);
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
