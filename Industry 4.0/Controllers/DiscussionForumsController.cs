using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Industry_4._0.Data;
using Industry_4._0.Models;
using Microsoft.AspNetCore.Authorization;

namespace Industry_4._0.Controllers
{
    public class DiscussionForumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscussionForumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiscussionForums
        public async Task<IActionResult> Comor()
        {
              return _context.DiscussionForum != null ? 
                          View(await _context.DiscussionForum.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DiscussionForum'  is null.");
        }

        // GET: DiscussionForums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DiscussionForum == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussionForum == null)
            {
                return NotFound();
            }

            return View(discussionForum);
        }

        // GET: DiscussionForums/Create
        [Authorize(Roles ="Manager, RegisteredUser")]
        public IActionResult Create()
        {
            DiscussionForum df = new DiscussionForum();
            df.PostDate = DateTime.Now;
            df.UserName = User.Identity.Name;
            return View(df);
        }

        // POST: DiscussionForums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Manager, RegisteredUser")]
        public async Task<IActionResult> Create([Bind("Id,PostDate,UserName,Heading,Like,TopicTitle,MessageContent,Agree, Disagree")] DiscussionForum discussionForum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discussionForum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Comor));
            }
            return View(discussionForum);
        }

        // GET: DiscussionForums/Edit/5
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DiscussionForum == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum.FindAsync(id);
            if (discussionForum == null)
            {
                return NotFound();
            }
            return View(discussionForum);
        }

        // POST: DiscussionForums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostDate,UserName,Heading,Like,TopicTitle,MessageContent,Agree,Disagree")] DiscussionForum discussionForum)
        {
            if (id != discussionForum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussionForum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionForumExists(discussionForum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Comor));
            }
            return View(discussionForum);
        }

        // GET: DiscussionForums/Delete/5
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DiscussionForum == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussionForum == null)
            {
                return NotFound();
            }

            return View(discussionForum);
        }

        // POST: DiscussionForums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DiscussionForum == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DiscussionForum'  is null.");
            }
            var discussionForum = await _context.DiscussionForum.FindAsync(id);
            if (discussionForum != null)
            {
                _context.DiscussionForum.Remove(discussionForum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Comor));
        }

        private bool DiscussionForumExists(int id)
        {
          return (_context.DiscussionForum?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //The shows the logic behind the actions of post rating
        public async Task<IActionResult> IncreaseLike(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum.FindAsync(id);
            if (discussionForum == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    discussionForum.Like++;

                    _context.Update(discussionForum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionForumExists(discussionForum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Comor));
            }
            return RedirectToAction(nameof(Comor));
        }

        //Display the algorithm for "agree" button on the post
        public async Task<IActionResult> Agree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum.FindAsync(id);
            if (discussionForum == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    discussionForum.Agree++;

                    _context.Update(discussionForum);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionForumExists(discussionForum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                    

                }
                return RedirectToAction(nameof(Comor));

            }
            return RedirectToAction(nameof(Comor));
        }

        //Display the algorithm for "disagree" button on the post
        public async Task<IActionResult> Disagree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum.FindAsync(id);
            if (discussionForum == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    discussionForum.Disagree++;

                    _context.Update(discussionForum);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionForumExists(discussionForum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }


                }
                return RedirectToAction(nameof(Comor));

            }
            return RedirectToAction(nameof(Comor));
        }




    }
}
