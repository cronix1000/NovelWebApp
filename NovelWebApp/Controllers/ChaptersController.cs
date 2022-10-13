using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NovelWebApp.Data;
using NovelWebApp.Models;

namespace NovelWebApp.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChaptersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chapters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Chapter.Include(c => c.Novel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Chapters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chapter == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapter
                .Include(c => c.Novel)
                .FirstOrDefaultAsync(m => m.ChapterId == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // GET: Chapters/Create
        //public IActionResult Create()
        //{
        //    ViewData["NovelId"] = new SelectList(_context.Novel, "NovelId", "Name");
        //    return View();
        //}
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null || _context.Novel == null)
            {
                return NotFound();
            }

            var novel = await _context.Novel.FindAsync(id);
            if (novel == null)
            {
                return NotFound();
            }
            ViewData["NovelId"] = new SelectList(_context.Novel, "NovelId", "Name", novel.NovelId);
            return View();
        }
        // POST: Chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChapterId,Title,ChapterNumber,chapterStory,NovelId")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chapter);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Index", controllerName: "Novels");
            }
            ViewData["NovelId"] = new SelectList(_context.Novel, "NovelId", "NovelId", chapter.NovelId);
            return View(chapter);
        }

        // GET: Chapters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chapter == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapter.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            ViewData["NovelId"] = new SelectList(_context.Novel, "NovelId", "Name", chapter.NovelId);
            return View(chapter);
        }

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChapterId,Title,ChapterNumber,chapterStory,NovelId")] Chapter chapter)
        {
            if (id != chapter.ChapterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chapter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChapterExists(chapter.ChapterId))
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
            ViewData["NovelId"] = new SelectList(_context.Novel, "NovelId", "NovelId", chapter.NovelId);
            return View(chapter);
        }

        // GET: Chapters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chapter == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapter
                .Include(c => c.Novel)
                .FirstOrDefaultAsync(m => m.ChapterId == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // POST: Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chapter == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Chapter'  is null.");
            }
            var chapter = await _context.Chapter.FindAsync(id);
            if (chapter != null)
            {
                _context.Chapter.Remove(chapter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChapterExists(int id)
        {
          return _context.Chapter.Any(e => e.ChapterId == id);
        }
    }
}
