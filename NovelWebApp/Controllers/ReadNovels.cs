﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelWebApp.Data;

namespace NovelWebApp.Controllers
{
    public class ReadNovels : Controller
    {    
        private readonly ApplicationDbContext _context;

        public ReadNovels(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Novel.ToListAsync());
        }
    }
}
