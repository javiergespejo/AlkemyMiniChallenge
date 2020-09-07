using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AlkemyMiniChallenge.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlkemyMiniChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var mostUsedTags = db.Tags.OrderByDescending(t => t.Videos.Count).Take(10);
            var operation = from s in _context.Operation.OrderByDescending(x => x.Id).Where(o => o.UserId == userId).Include(x => x.Category).Take(10)
                            select s;

            return View(operation);
        }
    }
}
