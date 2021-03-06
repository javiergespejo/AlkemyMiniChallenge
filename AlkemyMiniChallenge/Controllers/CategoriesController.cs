﻿using System;
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
using Microsoft.AspNetCore.Authorization;

namespace AlkemyMiniChallenge.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoriesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Categories
        [Authorize]
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            var userId = _userManager.GetUserId(User);
            var category = from s in _context.Category.Where(o => o.UserId == userId)
                            select s;

            category = sortOrder switch
            {
                "name_desc" => category.OrderByDescending(s => s.Name),
                _ => category.OrderBy(s => s.Name),
            };
            int pageSize = 10;
            return View(await PaginatedList<Category>.CreateAsync(category.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Categories/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            
            if (category.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Categories/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            var userId = _userManager.GetUserId(User);
            category.UserId = userId;

            if (ModelState.IsValid && CategoryNameExists(category.Name)!=true)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            if (category.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);            
            var existingCategory = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            if (existingCategory.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid && CategoryNameExists(category.Name) != true)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);
            
            if (category == null)
            {
                return NotFound();
            }

            if (category.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var category = await _context.Category.FindAsync(id);

            if (category.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }
           
            _context.Category.Remove(category);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }

        private bool CategoryNameExists(string name)
        {
            return _context.Category.Any(e => e.Name == name);
        }
    }
}
