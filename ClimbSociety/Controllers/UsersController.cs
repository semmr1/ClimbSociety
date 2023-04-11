using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Dynamic;
using ClimbSociety.Areas.Identity.Data;

namespace ClimbSociety.Controllers
{
    public class UsersController : Controller
    {
        private ClimbSocietyContext _context;

        public UsersController(ClimbSocietyContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var climbers = await _context.Climbers.ToListAsync();
            return View(climbers);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Climbers == null)
            {
                return NotFound();
            }

            var climbers = await _context.Climbers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (climbers == null)
            {
                return NotFound();
            }

            return View(climbers);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Moderator,Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Climbers == null)
            {
                return Problem("Entity set 'ClimbSocietyContext.Climbers' is null.");
            }
            var climber = await _context.Climbers.FindAsync(id);
            if (climber != null)
            {
                _context.Climbers.Remove(climber);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
