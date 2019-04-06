using GigHub.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace GigHub.Core.Controllers
{
    public class FolloweesController : Controller
    {

        private ApplicationDbContext _context;

        public FolloweesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Followees
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var artists = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            return View(artists);
        }
    }
}