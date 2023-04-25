using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using OnlineDiscussion.Areas.Identity.Data;
using OnlineDiscussion.Data;
using OnlineDiscussion.Models;
using Microsoft.AspNetCore.Identity;
namespace OnlineDiscussion.Controllers
{
    public class CommentViewsController : Controller
    {
        private readonly OnlineDiscussionContext _context;
        private readonly UserManager<OnlineDiscussionUser> _userManager;

        public CommentViewsController(OnlineDiscussionContext context, UserManager<OnlineDiscussionUser> manager)
        {
            _context = context;
            _userManager = manager;

        }

        // GET: CommentViews
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["topicid"] = id;
            Console.WriteLine("id :"+ViewData["topicid"]);
              return _context.CommentView != null ? 
                          View(await _context.CommentView
                          .Include(x =>x.user)
                          .Where(x =>x.topic.TopicId == id)
                          .ToListAsync()) :
                          Problem("Entity set 'OnlineDiscussionContext.CommentView'  is null.");
        }

        // GET: CommentViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CommentView == null)
            {
                return NotFound();
            }

            var commentView = await _context.CommentView
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (commentView == null)
            {
                return NotFound();
            }

            return View(commentView);
        }

        // GET: CommentViews/Create
        public IActionResult Create(int? id)
        {
            TempData["iid"] = id;
            return View();
        }

        // POST: CommentViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,CommentText,posteddate")] CommentView commentView)
        {
            int id = Convert.ToInt32(TempData["iid"]);
            Console.WriteLine(id);
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var userID = await _userManager.GetUserAsync(User);

            commentView.user = userID;
            try
            {
                var topicS = _context.TopicViewModel.Where(u => u.TopicId == id).FirstOrDefault();
                Console.WriteLine(topicS);
                commentView.topic = topicS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            _context.Add(commentView);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "CommentViews", new { id = id });


            return View();
        }

        // GET: CommentViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CommentView == null)
            {
                return NotFound();
            }

            var commentView = await _context.CommentView.FindAsync(id);
            if (commentView == null)
            {
                return NotFound();
            }
            return View(commentView);
        }

        // POST: CommentViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,CommentText,posteddate")] CommentView commentView)
        {
            if (id != commentView.CommentId)
            {
                return NotFound();
            }

          
                try
                {
                    _context.Update(commentView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentViewExists(commentView.CommentId))
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

        // GET: CommentViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CommentView == null)
            {
                return NotFound();
            }

            var commentView = await _context.CommentView
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (commentView == null)
            {
                return NotFound();
            }

            return View(commentView);
        }

        // POST: CommentViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CommentView == null)
            {
                return Problem("Entity set 'OnlineDiscussionContext.CommentView'  is null.");
            }
            var commentView = await _context.CommentView.FindAsync(id);
            if (commentView != null)
            {
                _context.CommentView.Remove(commentView);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentViewExists(int id)
        {
          return (_context.CommentView?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
