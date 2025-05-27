using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<NewSocks> NewSocks { get; set; } // Tabulka pro databázi
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
