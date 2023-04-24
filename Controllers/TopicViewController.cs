using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using OnlineDiscussion.Areas.Identity.Data;
using OnlineDiscussion.Data;
using OnlineDiscussion.Models;

namespace OnlineDiscussion.Controllers
{
    public class TopicViewController : Controller


    {
        private readonly OnlineDiscussionContext _context;

        private readonly UserManager<OnlineDiscussionUser> _userManager;

        public TopicViewController(OnlineDiscussionContext context, UserManager<OnlineDiscussionUser> manager)
        {
            _context = context;
            _userManager = manager;
        }

        // GET: TopicView
        public async Task<IActionResult> Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var userID = await _userManager.GetUserAsync(User);
            List<TopicViewModel> OrigUser = await _context.TopicViewModel
                        .Include(x => x.user)
                         .Where(x => x.user == userID)
                        .ToListAsync();
            List<TopicViewModel> sharedUsers = await _context.FriendsView
                            .Include(x => x.Topic)
                            .Include(x => x.Topic.user)
                            .Where(x => x.User == userID)
                            .Select(x=>x.Topic)
                            .ToListAsync();
            List<TopicViewModel> combinedList = OrigUser.Concat(sharedUsers).ToList();



            return View(combinedList);
        }

        // GET: TopicView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TopicViewModel == null)
            {
                return NotFound();
            }

            var topicViewModel = await _context.TopicViewModel
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topicViewModel == null)
            {
                return NotFound();
            }

            return View(topicViewModel);
        }

        // GET: TopicView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopicView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicId,TopicName,description,CreatedDate")] TopicViewModel topicViewModel)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            topicViewModel.user = user;
            _context.Add(topicViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            // return View(topicViewModel);
        }

        // GET: TopicView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TopicViewModel == null)
            {
                return NotFound();
            }

            var topicViewModel = await _context.TopicViewModel.FindAsync(id);
            if (topicViewModel == null)
            {
                return NotFound();
            }
            return View(topicViewModel);
        }

        // POST: TopicView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicId,TopicName,description,CreatedDate")] TopicViewModel topicViewModel)
        {
            if (id != topicViewModel.TopicId)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(topicViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicViewModelExists(topicViewModel.TopicId))
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

        // GET: TopicView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TopicViewModel == null)
            {
                return NotFound();
            }

            var topicViewModel = await _context.TopicViewModel
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topicViewModel == null)
            {
                return NotFound();
            }

            return View(topicViewModel);
        }

        // POST: TopicView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TopicViewModel == null)
            {
                return Problem("Entity set 'OnlineDiscussionContext.TopicViewModel'  is null.");
            }
            var topicViewModel = await _context.TopicViewModel.FindAsync(id);
            if (topicViewModel != null)
            {
                _context.TopicViewModel.Remove(topicViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicViewModelExists(int id)
        {
            return (_context.TopicViewModel?.Any(e => e.TopicId == id)).GetValueOrDefault();
        }
    }
}
