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
    public class ReadNovelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReadNovelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReadNovels
        public async Task<IActionResult> Index()
        {
              return View(await _context.Novel.ToListAsync());
        }

        // GET: ReadNovels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Novel == null)
            {
                return NotFound();
            }

            var novel = await _context.Novel.Include(n => n.Chapters)

                .FirstOrDefaultAsync(m => m.NovelId == id);
            if (novel == null)
            {
                return NotFound();
            }

            return View(novel);
        }

        // GET: ReadNovels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReadNovels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NovelId,Name,Description,Photo")] Novel novel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(novel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(novel);
        }

        // GET: ReadNovels/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(novel);
        }

        // POST: ReadNovels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NovelId,Name,Description,Photo")] Novel novel)
        {
            if (id != novel.NovelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovelExists(novel.NovelId))
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
            return View(novel);
        }

        // GET: ReadNovels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Novel == null)
            {
                return NotFound();
            }

            var novel = await _context.Novel
                .FirstOrDefaultAsync(m => m.NovelId == id);
            if (novel == null)
            {
                return NotFound();
            }

            return View(novel);
        }

        // POST: ReadNovels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Novel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Novel'  is null.");
            }
            var novel = await _context.Novel.FindAsync(id);
            if (novel != null)
            {
                _context.Novel.Remove(novel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovelExists(int id)
        {
          return _context.Novel.Any(e => e.NovelId == id);
        }
    }
}
