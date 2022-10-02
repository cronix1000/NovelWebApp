using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NovelWebApp.Models;

namespace NovelWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<NovelWebApp.Models.Novel> Novel { get; set; }
        public DbSet<NovelWebApp.Models.Chapter> Chapter { get; set; }
    }
}