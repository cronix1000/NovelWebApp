    using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NovelWebApp.Data;
using NovelWebApp.Models;

namespace NovelWebApp.Controllers
{
    [Authorize]
    public class NovelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NovelsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Novels
        public async Task<IActionResult> Index()
        {
              return View("Index", await _context.Novel.ToListAsync());
        }
        public IActionResult ReadNovel()
        {
            return View();
        }
        [AllowAnonymous]
        // GET: Novels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Novel == null)
            {
                return View("404");
            }

            var novel = await _context.Novel
                .FirstOrDefaultAsync(m => m.NovelId == id);
            if (novel == null)
            {
                return View("404");
            }

            return View(novel);
        }

        // GET: Novels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Novels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NovelId,Name,Description,MainTags")] Novel novel, IFormFile? Photo)
        {
            if (ModelState.IsValid)
            {
                // upload phpto if any before creating the new product in the db as product needs the Photo name
                if (Photo != null) {
                    var fileName = UploadPhoto(Photo);
                    novel.Photo = fileName;
                }
                _context.Add(novel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(novel);
        }

        // GET: Novels/Edit/5
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

        // POST: Novels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NovelId,Name,Description, MainTags")] Novel novel, IFormFile? Photo, string? CurrentPhoto)
        {
            if (id != novel.NovelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Photo != null)
                    {
                        var fileName = UploadPhoto(Photo);
                        novel.Photo = fileName;
                    }
                    else { 
                        novel.Photo = CurrentPhoto; 
                    }
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

        // GET: Novels/Delete/5
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

        // POST: Novels/Delete/5
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


        //Written Rich Freeman 
        private static string UploadPhoto(IFormFile Photo) {
            // get temp location of uploaded file
            var filePath = Path.GetTempFileName();

            // create unique name to prevent overwrites using Globally Unique Id class

            var fileName = Guid.NewGuid() + "-" + Photo.FileName;

            //set destination path to wwwroot/img/products
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\NovelPictures\\" + fileName;

            // copy the file to the targer dir
            using (var stream = new FileStream(uploadPath, FileMode.Create)) {
                Photo.CopyTo(stream);
            }

            return fileName;
        }
    }
}
