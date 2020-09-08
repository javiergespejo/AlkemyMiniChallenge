using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AlkemyMiniChallenge.Data;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var totalIncome = _context.Operation.Where(o => o.UserId == userId && o.Type == Models.TypeEnum.Income).Sum(x => x.Amount);
            var totalOutcome = _context.Operation.Where(o => o.UserId == userId && o.Type == Models.TypeEnum.Outcome).Sum(x => x.Amount);

            ViewBag.TotalBalance = totalIncome - totalOutcome;
            var operation = from s in _context.Operation.OrderByDescending(x => x.Id).Where(o => o.UserId == userId).Include(x => x.Category).Take(10)
                            select s;

            return View(operation);
        }
    }
}
