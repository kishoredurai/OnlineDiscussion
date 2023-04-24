using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineDiscussion.Areas.Identity.Data;
using OnlineDiscussion.Data;
using OnlineDiscussion.Models;

namespace OnlineDiscussion.Controllers
{
    public class FriendsViewsController : Controller
    {
        private readonly OnlineDiscussionContext _context;
        private readonly UserManager<OnlineDiscussionUser> _userManager;

        public FriendsViewsController(OnlineDiscussionContext context, UserManager<OnlineDiscussionUser> manager)
        {
            _context = context;
            _userManager = manager;

        }

        // GET: FriendsViews
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["topicid"] = id;
              return _context.FriendsView != null ? 
                          View(await _context.FriendsView
                          .Include(x =>x.Topic)
                          .Include(x =>x.User)
                          .Where(x => x.Topic.TopicId==id)
                          .ToListAsync()) :
                          Problem("Entity set 'OnlineDiscussionContext.FriendsView'  is null.");
        }

        // GET: FriendsViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FriendsView == null)
            {
                return NotFound();
            }

            var friendsView = await _context.FriendsView
                .FirstOrDefaultAsync(m => m.FriendId == id);
            if (friendsView == null)
            {
                return NotFound();
            }

            return View(friendsView);
        }

        // GET: FriendsViews/Create
        public IActionResult Create(int? id)
        {
            TempData["id"] = id;
            return View();
        }

        // POST: FriendsViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FriendId,email")] FriendsView friendsView)
        {
            
           var user =  _context.Users.Where(u=> u.Email==friendsView.email).FirstOrDefault();

            Console.WriteLine((int)TempData["id"]);

           var topic = _context.TopicViewModel.Where(u => u.TopicId == (int)TempData["id"]).FirstOrDefault();


            try
            {

                friendsView.User = user;
                friendsView.Topic = topic;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            _context.Add(friendsView);
            await _context.SaveChangesAsync();
           /* return RedirectToAction(nameof(Index));*/

            return RedirectToAction("Index", "FriendsViews", new { id = (int)TempData["id"] });
        }

        // GET: FriendsViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FriendsView == null)
            {
                return NotFound();
            }

            var friendsView = await _context.FriendsView.FindAsync(id);
            if (friendsView == null)
            {
                return NotFound();
            }
            return View(friendsView);
        }

        // POST: FriendsViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FriendId,email")] FriendsView friendsView)
        {
            if (id != friendsView.FriendId)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(friendsView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendsViewExists(friendsView.FriendId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "TopicViewController");

            
        }

        // GET: FriendsViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FriendsView == null)
            {
                return NotFound();
            }

            var friendsView = await _context.FriendsView
                .FirstOrDefaultAsync(m => m.FriendId == id);
            if (friendsView == null)
            {
                return NotFound();
            }

            return View(friendsView);
        }

        // POST: FriendsViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FriendsView == null)
            {
                return Problem("Entity set 'OnlineDiscussionContext.FriendsView'  is null.");
            }
            var friendsView = await _context.FriendsView.FindAsync(id);
            if (friendsView != null)
            {
                _context.FriendsView.Remove(friendsView);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "TopicViewController");

        }

        private bool FriendsViewExists(int id)
        {
          return (_context.FriendsView?.Any(e => e.FriendId == id)).GetValueOrDefault();
        }
    }
}
