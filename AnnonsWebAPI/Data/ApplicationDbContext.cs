using AnnonsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnonsWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ad> Ads { get; set; }
    }

}
