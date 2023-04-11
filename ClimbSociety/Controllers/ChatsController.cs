using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClimbSociety.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using ClimbSociety.Areas.Identity.Data;

namespace ClimbSociety
{
    public class ChatsController : Controller
    {
        private readonly ClimbSocietyContext _context;

        public ChatsController(ClimbSocietyContext context)
        {
            _context = context;
            
        }

        public class ChatHub : Hub
        {
            public async Task SendPrivateMessage(string user, string message, string connId)
            {
                //await Clients.All.SendAsync("ReceiveMessage", user, message);
                await Clients.Client(connId).SendAsync("ReceiveMessage", user, message);
            }

            public async Task SendMessageToGroup(string groupName, string message, string user)
            {
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
            }

            public async Task AddToGroup(string groupName, string user)
            {
                //await _userManager.FindByIdAsync();
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

                await Clients.Group(groupName).SendAsync("JoinedOrLeft", $"{user} has joined the group {groupName}.");
            }

            public async Task RemoveFromGroup(string groupName)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

                await Clients.Group(groupName).SendAsync("JoinedOrLeft", $"{Context.ConnectionId} has left the group {groupName}.");
            }
        }

        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }

        //// GET: Chat
        //public async Task<IActionResult> Index()
        //{
        //      return _context.ChatMessage != null ? 
        //                  View(await _context.ChatMessage.ToListAsync()) :
        //                  Problem("Entity set 'ClimbSocietyContext.ChatMessage'  is null.");
        //}

        //// GET: Chat/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.ChatMessage == null)
        //    {
        //        return NotFound();
        //    }

        //    var chatMessage = await _context.ChatMessage
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (chatMessage == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(chatMessage);
        //}

        //// GET: Chat/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Chat/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,User,ConnectionId,Message,Time")] ChatMessage chatMessage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(chatMessage);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(chatMessage);
        //}

        //// GET: Chat/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.ChatMessage == null)
        //    {
        //        return NotFound();
        //    }

        //    var chatMessage = await _context.ChatMessage.FindAsync(id);
        //    if (chatMessage == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(chatMessage);
        //}

        //// POST: Chat/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,User,ConnectionId,Message,Time")] ChatMessage chatMessage)
        //{
        //    if (id != chatMessage.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(chatMessage);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ChatMessageExists(chatMessage.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(chatMessage);
        //}

        //// GET: Chat/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.ChatMessage == null)
        //    {
        //        return NotFound();
        //    }

        //    var chatMessage = await _context.ChatMessage
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (chatMessage == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(chatMessage);
        //}

        //// POST: Chat/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.ChatMessage == null)
        //    {
        //        return Problem("Entity set 'ClimbSocietyContext.ChatMessage'  is null.");
        //    }
        //    var chatMessage = await _context.ChatMessage.FindAsync(id);
        //    if (chatMessage != null)
        //    {
        //        _context.ChatMessage.Remove(chatMessage);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ChatMessageExists(int id)
        //{
        //  return (_context.ChatMessage?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
