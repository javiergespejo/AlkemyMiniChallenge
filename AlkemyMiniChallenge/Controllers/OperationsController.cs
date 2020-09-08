using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlkemyMiniChallenge.Data;
using AlkemyMiniChallenge.Models;
using AlkemyMiniChallenge.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AlkemyMiniChallenge.Controllers
{
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OperationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Operations
        [Authorize]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var userId = _userManager.GetUserId(User);
            var operation = from s in _context.Operation.Where(o => o.UserId == userId).Include(x => x.Category)
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                operation = operation.Where(s => s.Concept.Contains(searchString));
            }

            operation = sortOrder switch
            {
                "Date" => operation.OrderBy(s => s.Date),
                "date_desc" => operation.OrderByDescending(s => s.Date),
                _ => operation.OrderBy(s => s.Date),
            };

            int pageSize = 10;
            return View(await PaginatedList<Operation>.CreateAsync(operation.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Operations/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var operation = await _context.Operation
                .Include(o => o.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (operation == null)
            {
                return NotFound();
            }

            if (operation.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(operation);
        }

        // GET: Operations/Create
        [Authorize]
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["CategoryId"] = new SelectList(_context.Category.Where(c => c.UserId == userId), "Id", "Name");
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Concept,Amount,Type,Date,CategoryId")] Operation operation)
        {
            var userId = _userManager.GetUserId(User);
            operation.UserId = userId;

            if (ModelState.IsValid)
            {
                _context.Add(operation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category.Where(c => c.UserId == userId), "Id", "Id", operation.CategoryId);
            return View(operation);
        }

        // GET: Operations/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var operation = await _context.Operation.FindAsync(id);

            if (operation == null)
            {
                return NotFound();
            }

            if (operation.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", operation.CategoryId);
            return View(operation);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Concept,Amount,Type,Date,CategoryId")] Operation operation)
        {
            if (id != operation.Id)
            {
                return NotFound();
            }
           
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var existingOperation = await _context.Operation.FindAsync(id);

            if (existingOperation.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationExists(operation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", operation.CategoryId);
            return View(operation);
        }

        // GET: Operations/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var operation = await _context.Operation
                .Include(o => o.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (operation == null)
            {
                return NotFound();
            }
            
            if (operation.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var operation = await _context.Operation.FindAsync(id);

            if (operation.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Operation.Remove(operation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationExists(int id)
        {
            return _context.Operation.Any(e => e.Id == id);
        }
    }
}
