using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClimbSociety.Data;
using ClimbSociety.Models;
using Microsoft.AspNetCore.Identity;
using ClimbSociety.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClimbSociety.Controllers
{
    [Authorize]
    public class MatchesController : Controller
    {
        private readonly ClimbSocietyContext _context;
        private readonly UserManager<Climber> _userManager;

        public MatchesController(ClimbSocietyContext context, UserManager<Climber> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public void FindMatches()
        {
            var currentUser = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var userClimbingLevel = currentUser.ClimbingLevel;
            var climbersMatchingLevels = _context.Users.Where(c => c.ClimbingLevel == userClimbingLevel).ToList();
            foreach(var partnerClimber in climbersMatchingLevels)
            {
                var match = new Match
                {
                    YourId = currentUser.Id,
                    PartnerId = partnerClimber.Id
                };
                _context.Matches.Add(match);
            }
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            return _context.Matches != null ?
                        View(await _context.Matches.ToListAsync()) :
                        Problem("Entity set 'ClimbSocietyContext.Matches'  is null.");
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PartnerId")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PartnerId")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'ClimbSocietyContext.Matches'  is null.");
            }
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(string id)
        {
            return (_context.Matches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
